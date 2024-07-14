// Copyright 2024 Cencora. All rights reserved.
//
// Written by Felix Kahle, A123234, felix.kahle@worldcourier.de

using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Cencora.TimeVault.DataTransferObjects.Common;

/// <summary>
/// Represents an address DTO that provides information about an address.
/// </summary>
public record AddressDto
{
    /// <summary>
    /// Represents an empty address DTO.
    /// </summary>
    public static readonly AddressDto Empty = new()
    {
        AddressLine1 = string.Empty,
        AddressLine2 = string.Empty,
        City = string.Empty,
        StateOrProvince = string.Empty,
        PostalCode = string.Empty,
        Country = string.Empty
    };
            
        
    /// <summary>
    /// Gets or sets the address line 1.
    /// </summary>
    [JsonInclude]
    public required string AddressLine1 { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the address line 2.
    /// </summary>
    [JsonInclude]
    public required string AddressLine2 { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the city.
    /// </summary>
    [JsonInclude]
    public required string City { get; init; } = string.Empty;
        
    /// <summary>
    /// Gets or sets the state or province.
    /// </summary>
    [JsonInclude]
    public required string StateOrProvince { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the postal code.
    /// </summary>
    [JsonInclude]
    public required string PostalCode { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the country.
    /// </summary>
    [JsonInclude]
    public required string Country { get; init; } = string.Empty;

    /// <summary>
    /// Gets a value indicating whether the address is empty.
    /// </summary>
    [JsonIgnore]
    [BindNever]
    public bool IsEmpty => string.IsNullOrEmpty(AddressLine1) &&
                           string.IsNullOrEmpty(AddressLine2) &&
                           string.IsNullOrEmpty(City) &&
                           string.IsNullOrEmpty(StateOrProvince) &&
                           string.IsNullOrEmpty(PostalCode) &&
                           string.IsNullOrEmpty(Country);

    /// <inheritdoc/>
    public override string ToString()
    {
        string[] parts =
        [
            AddressLine1,
            AddressLine2,
            City,
            StateOrProvince,
            PostalCode,
            Country
        ];

        return string.Join(", ", parts.Where(p => !string.IsNullOrEmpty(p)));
    }
}