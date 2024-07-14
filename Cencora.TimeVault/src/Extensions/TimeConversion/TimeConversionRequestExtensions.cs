// Copyright 2024 Cencora. All rights reserved.
//
// Written by Felix Kahle, A123234, felix.kahle@worldcourier.de

using System.Text.Json;
using Cencora.TimeVault.DataTransferObjects.TimeConversion;
using Cencora.TimeVault.Models.TimeConversion;

namespace Cencora.TimeVault.Extensions.TimeConversion;

/// <summary>
/// Provides extension methods for <see cref="TimeConversionRequest"/> and <see cref="TimeConversionRequestDto"/>.
/// </summary>
public static class TimeConversionRequestExtensions
{
    /// <summary>
    /// Converts a <see cref="TimeConversionRequest"/> to a <see cref="TimeConversionRequestDto"/>.
    /// </summary>
    /// <param name="request">The request to convert.</param>
    /// <returns>The converted request.</returns>
    public static TimeConversionRequestDto ToDto(this TimeConversionRequest request)
    {
        return new TimeConversionRequestDto
        {
            OriginTimeZoneId = request.OriginTimeZone.Id,
            DestinationTimeZoneId = request.DestinationTimeZone.Id,
            Time = JsonSerializer.Serialize(request.Time)
        };
    }

    /// <summary>
    /// Converts a <see cref="TimeConversionRequestDto"/> to a <see cref="TimeConversionRequest"/>.
    /// </summary>
    /// <param name="request">The request to convert.</param>
    /// <returns>The converted request.</returns>
    public static TimeConversionRequest ToModel(this TimeConversionRequestDto request)
    {
        return new TimeConversionRequest
        {
            OriginTimeZone = TimeZoneInfo.FindSystemTimeZoneById(request.OriginTimeZoneId),
            DestinationTimeZone = TimeZoneInfo.FindSystemTimeZoneById(request.DestinationTimeZoneId),
            Time = JsonSerializer.Deserialize<DateTime>(request.Time)
        };
    }
}