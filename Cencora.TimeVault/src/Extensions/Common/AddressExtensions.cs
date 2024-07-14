// Copyright 2024 Cencora. All rights reserved.
//
// Written by Felix Kahle, A123234, felix.kahle@worldcourier.de

using System.Globalization;
using Cencora.Common.Maps;
using Cencora.TimeVault.DataTransferObjects.Common;

namespace Cencora.TimeVault.Extensions.Common;

/// <summary>
/// Extensions for the <see cref="Address"/> class.
/// </summary>
public static class AddressExtensions
{
    /// <summary>
    /// Converts the address to a DTO.
    /// </summary>
    /// <param name="address">The address to convert.</param>
    /// <returns>The DTO.</returns>
    public static AddressDto ToDto(this Address address)
    {
        return new AddressDto
        {
            AddressLine1 = address.AddressLine1,
            AddressLine2 = address.AddressLine2,
            City = address.City,
            StateOrProvince = address.StateOrProvince,
            PostalCode = address.PostalCode,
            Country = address.Country
        };
    }
        
    /// <summary>
    /// Converts the address DTO to a model.
    /// </summary>
    /// <param name="addressDto">The address DTO to convert.</param>
    /// <returns>The model.</returns>
    public static Address ToModel(this AddressDto addressDto)
    {
        return new Address
        {
            AddressLine1 = addressDto.AddressLine1,
            AddressLine2 = addressDto.AddressLine2,
            City = addressDto.City,
            StateOrProvince = addressDto.StateOrProvince,
            PostalCode = addressDto.PostalCode,
            Country = addressDto.Country
        };
    }
        
    /// <summary>
    /// Converts the address to a formatted string.
    /// </summary>
    /// <param name="address">The address to convert.</param>
    /// <returns>The formatted string.</returns>
    public static string ToFormattedString(this Address address)
    {
        string[] parts =
        [
            address.AddressLine1,
            address.AddressLine2,
            address.City,
            address.StateOrProvince,
            address.PostalCode,
            address.Country
        ];

        return string.Join(", ", parts.Where(p => !string.IsNullOrEmpty(p)));
    }

    /// <summary>
    /// Checks if the country is a valid ISO code.
    /// </summary>
    /// <param name="address">The address to check.</param>
    /// <returns><c>true</c> if the country is a valid ISO code; otherwise, <c>false</c>.</returns>
    public static bool IsCountryValidIsoCode(this Address address)
    {
        var country = address.Country;
        if (string.IsNullOrWhiteSpace(country))
        {
            return false;
        }

        return CultureInfo
            .GetCultures(CultureTypes.SpecificCultures)
            .Select(culture => new RegionInfo(culture.LCID))
            .Any(region => region.TwoLetterISORegionName == country);
    }
}