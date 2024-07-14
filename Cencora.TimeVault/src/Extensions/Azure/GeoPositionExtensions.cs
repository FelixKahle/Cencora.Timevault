// Copyright 2024 Cencora. All rights reserved.
//
// Written by Felix Kahle, A123234, felix.kahle@worldcourier.de

using Azure.Core.GeoJson;
using Cencora.Common.Maps;

namespace Cencora.TimeVault.Extensions.Azure;

public static class GeoPositionExtensions
{
    /// <summary>
    /// Converts the specified <see cref="GeoPosition"/> to a <see cref="GeoCoordinate"/>.
    /// </summary>
    /// <param name="geoPosition">The <see cref="GeoPosition"/> to convert.</param>
    /// <returns>The <see cref="GeoCoordinate"/> converted from the specified <see cref="GeoPosition"/>.</returns>
    public static GeoCoordinate ToGeoCoordinate(this GeoPosition geoPosition)
    {
        return new GeoCoordinate
        {
            Latitude = geoPosition.Latitude,
            Longitude = geoPosition.Longitude
        };
    }
}