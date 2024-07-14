// Copyright 2024 Cencora. All rights reserved.
//
// Written by Felix Kahle, A123234, felix.kahle@worldcourier.de

using System.Text.Json;
using Cencora.TimeVault.DataTransferObjects.Timezone;
using Cencora.TimeVault.Models.Timezone;

namespace Cencora.TimeVault.Extensions.Timezone;

/// <summary>
/// Provides extension methods for converting between <see cref="TimezoneResponse"/> and <see cref="TimeZoneResponseDto"/>.
/// </summary>
public static class TimeZoneResponseExtensions
{
    /// <summary>
    /// Converts a <see cref="TimezoneResponse"/> object to a <see cref="TimeZoneResponseDto"/> object.
    /// </summary>
    /// <param name="response">The <see cref="TimezoneResponse"/> object to convert.</param>
    /// <returns>A new <see cref="TimeZoneResponseDto"/> object.</returns>
    public static TimeZoneResponseDto ToDto(this TimezoneResponse response)
    {
        return new TimeZoneResponseDto
        {
            StatusCode = response.StatusCode,
            Address = response.Address,
            RequestedAt = JsonSerializer.Serialize(response.RequestedAt),
            TimezoneId = response.TimeZone == null ? string.Empty : response.TimeZone.Id
        };
    }

    /// <summary>
    /// Converts a <see cref="TimeZoneResponseDto"/> object to a <see cref="TimezoneResponse"/> object.
    /// </summary>
    /// <param name="dto">The <see cref="TimeZoneResponseDto"/> object to convert.</param>
    /// <returns>A new <see cref="TimezoneResponse"/> object.</returns>
    public static TimezoneResponse ToModel(this TimeZoneResponseDto dto)
    {
        return new TimezoneResponse
        {
            StatusCode = dto.StatusCode,
            Address = dto.Address,
            RequestedAt = JsonSerializer.Deserialize<DateTimeOffset>(dto.RequestedAt),
            TimeZone = string.IsNullOrWhiteSpace(dto.TimezoneId) ? null : TimeZoneInfo.FindSystemTimeZoneById(dto.TimezoneId)
        };
    }
}