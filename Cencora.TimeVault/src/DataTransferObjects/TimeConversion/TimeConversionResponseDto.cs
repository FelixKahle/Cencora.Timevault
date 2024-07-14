// Copyright 2024 Cencora. All rights reserved.
//
// Written by Felix Kahle, A123234, felix.kahle@worldcourier.de

using System.Text.Json.Serialization;
using Cencora.TimeVault.DataTransferObjects.Common;

namespace Cencora.TimeVault.DataTransferObjects.TimeConversion;

/// <summary>
/// Represents a response for a time conversion.
/// </summary>
public record TimeConversionResponseDto : ResponseDto
{
    /// <summary>
    /// Gets or sets the origin timezone.
    /// </summary>
    [JsonInclude]
    public required string OriginTimeZoneId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the destination timezone.
    /// </summary>
    [JsonInclude]
    public required string DestinationTimeZoneId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the original time.
    /// </summary>
    [JsonInclude]
    public required string OriginalTime { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the converted time.
    /// </summary>
    [JsonInclude]
    public required string ConvertedTime { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the time when the request was made.
    /// </summary>
    [JsonInclude]
    public required string RequestedAt { get; set; } = string.Empty;

    /// <inheritdoc />
    public override string ToString()
    {
        return $"{OriginalTime} {OriginTimeZoneId} -> {ConvertedTime} {DestinationTimeZoneId}";
    }
}