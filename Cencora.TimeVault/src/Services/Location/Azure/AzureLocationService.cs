// Copyright 2024 Cencora. All rights reserved.
//
// Written by Felix Kahle, A123234, felix.kahle@worldcourier.de

using System.Net;
using Azure;
using Azure.Core.GeoJson;
using Azure.Maps.Search;
using Azure.Maps.Search.Models;
using Cencora.Common.Api;
using Cencora.Common.Maps;
using Cencora.TimeVault.Extensions.Azure;
using Cencora.TimeVault.Extensions.Common;

namespace Cencora.TimeVault.Services.Location.Azure;

/// <summary>
/// Represents a service for working with locations using the Azure Maps service.
/// </summary>
/// <param name="logger">The logger to use for logging.</param>
/// <param name="mapsSearchClient">The Azure Maps search client to use for searching addresses.</param>
public class AzureLocationService(ILogger<AzureLocationService> logger, MapsSearchClient mapsSearchClient)
    : ILocationService
{
    private const int SearchAddressSyncBatchMaxSize = 100;
    private const int SearchAddressAsyncBatchMaxSize = 10000;
    private const int SearchAddressDefaultTop = 1;

    /// <inheritdoc/>
    public async Task<ApiResponse<GeoCoordinate>> GetGeoCoordinateAsync(Address address)
    {
        var response = await GetGeoPositionAsync(address);
        return response.Into(g => g.ToGeoCoordinate());
    }

    /// <summary>
    /// Gets the geo position for the specified address.
    /// </summary>
    /// <param name="address">The address to get the geo position for.</param>
    /// <returns>The geo position for the specified address.</returns>
    private async Task<ApiResponse<GeoPosition>> GetGeoPositionAsync(Address address)
    {
        try
        {
            var response = await AzureSearchAddressAsync(address);
            if (response.Results.Count == 0)
            {
                logger.LogWarning("The address {address} could not be found.", address);
                return ApiResponse<GeoPosition>.Error(HttpStatusCode.NotFound, $"The address {address} could not be found.");
            }
            var result = response.Results.OrderByDescending(result => result.Score).First();
            return ApiResponse<GeoPosition>.Success(result.Position);
        }
        catch (RequestFailedException ex)
        {
            return ApiResponse<GeoPosition>.Error(ex.Status, ex.ErrorCode ?? ex.Message);
        }
    }

    /// <summary>
    /// Gets the geo positions for the specified addresses.
    /// </summary>
    /// <param name="addresses">The addresses to get the geo positions for.</param>
    /// <returns>The geo positions for the specified addresses.</returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="addresses"/> is <c>null</c> or empty.</exception>
    // ReSharper disable once UnusedMember.Local
    private async Task<List<ApiResponse<GeoPosition>>> GetGeoPositionsBatchAsync(List<Address> addresses)
    {
        // Validate the addresses.
        ArgumentNullException.ThrowIfNull(addresses, nameof(addresses));
        var addressesCount = addresses.Count;

        // Split the addresses into chunks, so that we do not exceed the maximum batch size
        // of the Azure Maps service.
        var chunks = addresses
            .Chunk(SearchAddressAsyncBatchMaxSize)
            .Select(x => x.ToList())
            .ToList();
            
        var results = new List<ApiResponse<GeoPosition>>(addressesCount);
        foreach (var chunk in chunks)
        {
            try
            {
                var response = await AzureSearchAddressBatchAsync(chunk);

                // Check if we have any results at all.
                if (response.Results.Count == 0)
                {
                    return chunk.Select(address => ApiResponse<GeoPosition>.Error(HttpStatusCode.NotFound, $"The address {address} could not be found.")).ToList();
                }

                // Map the results to the ApiResponse.
                var chunkResults = response.Results
                    .Select(batchItem =>
                    {
                        // We did not find any results for the query.
                        // In this case we want to log what happened and return an error response.
                        if (batchItem.Results.Count == 0)
                        {
                            logger.LogWarning("The address {items} could not be found.", batchItem.Query);
                            return ApiResponse<GeoPosition>.Error(HttpStatusCode.NotFound, $"The address {batchItem.Query} could not be found.");
                        }

                        var item = batchItem.Results.OrderByDescending(item => item.Score).First();
                        return ApiResponse<GeoPosition>.Success(item.Position);
                    })
                    .ToList();
                results.AddRange(chunkResults);
            }
            catch (RequestFailedException ex)
            {
                results.AddRange(chunk.Select(_ => ApiResponse<GeoPosition>.Error(ex.Status, ex.ErrorCode ?? ex.Message)));
            }
        }

        return results;
    }

    /// <summary>
    /// Gets the timezone for the specified address.
    /// </summary>
    /// <param name="address">The address to get the timezone for.</param>
    /// <param name="top">The maximum number of results to return.</param>
    /// <returns>The timezone for the specified address.</returns>
    private async Task<SearchAddressResult> AzureSearchAddressAsync(Address address, int top = SearchAddressDefaultTop)
    {
        var options = new SearchAddressOptions
        {
            CountryFilter = address.IsCountryValidIsoCode() ? new List<string> { address.Country } : [],
            Top = top
        };

        var queryString = address.ToAzureQueryString();
        // Validate the query. We do not want to send empty queries 
        // to the Azure Maps service.
        ArgumentException.ThrowIfNullOrWhiteSpace(queryString, nameof(queryString));

        // Log some information about the search.
        logger.LogInformation("Performing a search for the address {address}.", address);

        SearchAddressResult response = await mapsSearchClient.SearchAddressAsync(queryString, options);
        return response;
    }

    /// <summary>
    /// Performs a batch search for the specified addresses.
    /// </summary>
    /// <param name="addresses">The addresses to search for.</param>
    /// <param name="top">The maximum number of results to return per address.</param>
    /// <returns>The search result for the specified addresses.</returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="addresses"/> is <c>null</c> or empty.</exception>
    /// <exception cref="AzureMapsException">Thrown if an error occurs in the Azure Maps service.</exception>
    private async Task<SearchAddressBatchResult> AzureSearchAddressBatchAsync(List<Address> addresses, int top = SearchAddressDefaultTop)
    {
        ArgumentNullException.ThrowIfNull(addresses, nameof(addresses));

        // Create the queries for the batch search.
        var queries = addresses
            .Select(address => new SearchAddressQuery(address.ToAzureQueryString(), new SearchAddressOptions
            {
                CountryFilter = address.IsCountryValidIsoCode() ? new List<string> { address.Country } : [],
                Top = top
            }))
            .ToList();

        // Precompute the number of queries.
        var queriesCount = queries.Count;
        
        logger.LogInformation("Performing a batch search for {queriesCount} addresses.", queriesCount);

        switch (queriesCount)
        {
            // In this case we can use the synchronous method,
            // as for small number of queries the synchronous method is more efficient.
            case <= SearchAddressSyncBatchMaxSize:
            {
                var syncBatchResponse = await mapsSearchClient.SearchAddressBatchAsync(WaitUntil.Completed, queries);
                if (syncBatchResponse.HasValue == false)
                {
                    throw new AzureMapsException("The search address batch operation has no value.");
                }
                return syncBatchResponse.Value;
            }
            // In this case we have to use the asynchronous method,
            // as for large number of queries the synchronous method is not supported.
            case <= SearchAddressAsyncBatchMaxSize:
            {
                var asyncBatchResponse = await mapsSearchClient.SearchAddressBatchAsync(WaitUntil.Completed, queries);
                if (asyncBatchResponse.HasValue == false)
                {
                    throw new AzureMapsException("The search address batch operation has no value.");
                }
                return asyncBatchResponse.Value;
            }
            default:
                throw new AzureMapsException($"The number {queriesCount} of queries exceeds the maximum batch size of {SearchAddressAsyncBatchMaxSize}.");
        }
    }
}