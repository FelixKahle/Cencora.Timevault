// Copyright 2024 Cencora. All rights reserved.
//
// Written by Felix Kahle, A123234, felix.kahle@worldcourier.de

using Cencora.TimeVault.Models.Common;
using Cencora.TimeVault.Models.Timezone;
using Cencora.TimeVault.Services.Timezone.TimezoneRetrieval;

namespace Cencora.TimeVault.Services.Timezone;

/// <summary>
/// Services that acts as a pipeline for retrieving the timezone.
/// </summary>
/// <remarks>
/// Under the hood, this service uses the <see cref="ITimezoneRetrievalService"/> to retrieve the timezone,
/// and propagates the result to the caller.
/// </remarks>
public class RetrievalPipelineTimeZoneService(ITimezoneRetrievalService retrievalService) : ITimeZoneService
{
    /// <inheritdoc/>
    public async Task<TimezoneResponse> GetTimezoneAsync(TimeZoneRequest request)
    {
        var requestedAt = DateTimeOffset.UtcNow;
        var address = request.Address;
        var response = await retrievalService.GetTimezoneAsync(address);

        return response.Match(
            success: (value, statusCode) => new TimezoneResponse
            {
                RequestedAt = requestedAt,
                Address = address,
                TimeZone = value,
                StatusCode = statusCode
            },
            error: (statusCode, errorMessage) => new TimezoneResponse
            {
                RequestedAt = requestedAt,
                Address = address,
                StatusCode = statusCode,
                Error = new ErrorDetail
                {
                    Message = string.IsNullOrEmpty(errorMessage) ? errorMessage : "An error occurred while retrieving the timezone.",
                    ErrorCode = "TimezoneRetrievalError"
                }
            }
        );
    }
}