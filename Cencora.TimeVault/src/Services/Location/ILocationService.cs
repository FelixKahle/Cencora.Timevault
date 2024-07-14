// Copyright 2024 Cencora. All rights reserved.
//
// Written by Felix Kahle, A123234, felix.kahle@worldcourier.de

using Cencora.Common.Api;
using Cencora.Common.Maps;

namespace Cencora.TimeVault.Services.Location;

/// <summary>
/// Service for working with locations.
/// </summary>
public interface ILocationService
{
    /// <summary>
    /// Gets the geo coordinate for the given address.
    /// </summary>
    /// <param name="address">The address to get the geo coordinate for.</param>
    /// <returns>The geo coordinate for the given address.</returns>
    public Task<ApiResponse<GeoCoordinate>> GetGeoCoordinateAsync(Address address);
}