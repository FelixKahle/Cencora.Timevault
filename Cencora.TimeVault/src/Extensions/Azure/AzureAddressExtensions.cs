// Copyright 2024 Cencora. All rights reserved.
//
// Written by Felix Kahle, A123234, felix.kahle@worldcourier.de

using Cencora.Common.Maps;

namespace Cencora.TimeVault.Extensions.Azure;

/// <summary>
/// Extensions for the <see cref="Address"/> for Azure Maps.
/// </summary>
public static class AzureAddressExtensions
{
    /// <summary>
    /// Converts the address to a query string that can be used with Azure Maps.
    /// </summary>
    /// <param name="address">The address to convert.</param>
    /// <returns>The query string that can be used with Azure Maps.</returns>
    public static string ToAzureQueryString(this Address address)
    {
        string[] parts =
        [
            address.AddressLine1,
            address.AddressLine2,
            address.City,
            address.StateOrProvince,
            // For whatever reason, the postal code in the query string should not contain the dash.
            // Azure Maps does not seem to find the location if the dash is included.
            address.PostalCode.Replace("-", string.Empty),
            address.Country
        ];

        return string.Join(", ", parts.Where(p => !string.IsNullOrEmpty(p)));
    }
}