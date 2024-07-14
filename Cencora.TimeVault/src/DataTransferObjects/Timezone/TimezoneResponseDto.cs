// Copyright 2024 Cencora. All rights reserved.
//
// Written by Felix Kahle, A123234, felix.kahle@worldcourier.de

using System.Text.Json.Serialization;
using Cencora.Common.Maps;
using Cencora.TimeVault.DataTransferObjects.Common;
using Cencora.TimeVault.Utils;

namespace Cencora.TimeVault.DataTransferObjects.Timezone;

/// <summary>
/// Represents a response DTO for a timezone request.
/// </summary>
public record TimeZoneResponseDto : ResponseDto
{
    /// <summary>
    /// Gets or sets the timezone result.
    /// </summary>
    /// <remarks>
    /// If no timezone was found, this property is set to <see cref="string.Empty"/>.
    /// </remarks>
    [JsonInclude]
    public required string TimezoneId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the address.
    /// </summary>
    /// <remarks>
    /// If the address is empty, it is ignored during serialization.
    /// </remarks>
    [JsonInclude]
    [JsonConverter(typeof(IgnoreEmptyAddressConverter))]
    public required Address Address { get; init; }

    /// <summary>
    /// Gets or sets the requested timestamp.
    /// </summary>
    [JsonInclude]
    public string RequestedAt { get; set; } = string.Empty;

    /// <inheritdoc/>
    public override string ToString()
    {
        var timezoneId = string.IsNullOrWhiteSpace(TimezoneId) ? "Unknown" : TimezoneId;
        return $"{Address}: {timezoneId}";
    }
}