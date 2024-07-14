// Copyright 2024 Cencora. All rights reserved.
//
// Written by Felix Kahle, A123234, felix.kahle@worldcourier.de

using System.Text.Json;
using Cencora.TimeVault.DataTransferObjects.TimeConversion;
using Cencora.TimeVault.Models.TimeConversion;

namespace Cencora.TimeVault.Extensions.TimeConversion;

/// <summary>
/// Provides extension methods for <see cref="TimeConversionResponse"/> and <see cref="TimeConversionResponseDto"/>.
/// </summary>
public static class TimeConversionResponseExtensions
{
    /// <summary>
    /// Converts a <see cref="TimeConversionResponse"/> to a <see cref="TimeConversionResponseDto"/>.
    /// </summary>
    /// <param name="response">The response to convert.</param>
    /// <returns>The converted response.</returns>
    public static TimeConversionResponseDto ToDto(this TimeConversionResponse response)
    {
        return new TimeConversionResponseDto
        {
            StatusCode = response.StatusCode,
            OriginTimeZoneId = response.OriginTimeZone?.Id ?? string.Empty,
            DestinationTimeZoneId = response.DestinationTimeZone?.Id ?? string.Empty,
            OriginalTime = response.OriginalTime.HasValue == false ? string.Empty : JsonSerializer.Serialize(response.OriginalTime.Value),
            ConvertedTime = response.ConvertedTime.HasValue == false ? string.Empty : JsonSerializer.Serialize(response.ConvertedTime.Value),
            RequestedAt = JsonSerializer.Serialize(response.RequestedAt)
        };
    }

    /// <summary>
    /// Converts a <see cref="TimeConversionResponseDto"/> to a <see cref="TimeConversionResponse"/>.
    /// </summary>
    /// <param name="response">The response to convert.</param>
    /// <returns>The converted response.</returns>
    public static TimeConversionResponse ToModel(this TimeConversionResponseDto response)
    {
        return new TimeConversionResponse
        {
            StatusCode = response.StatusCode,
            OriginTimeZone = string.IsNullOrWhiteSpace(response.OriginTimeZoneId) ? null : TimeZoneInfo.FindSystemTimeZoneById(response.OriginTimeZoneId),
            DestinationTimeZone = string.IsNullOrWhiteSpace(response.DestinationTimeZoneId) ? null : TimeZoneInfo.FindSystemTimeZoneById(response.DestinationTimeZoneId),
            OriginalTime = string.IsNullOrWhiteSpace(response.OriginalTime) ? null : JsonSerializer.Deserialize<DateTime>(response.OriginalTime),
            ConvertedTime = string.IsNullOrWhiteSpace(response.ConvertedTime) ? null : JsonSerializer.Deserialize<DateTime>(response.ConvertedTime),
            RequestedAt = string.IsNullOrWhiteSpace(response.RequestedAt) ? DateTimeOffset.UtcNow : JsonSerializer.Deserialize<DateTimeOffset>(response.RequestedAt)
        };
    }
}