// Copyright 2024 Cencora. All rights reserved.
//
// Written by Felix Kahle, A123234, felix.kahle@worldcourier.de

using Cencora.TimeVault.DataTransferObjects.Timezone;
using Cencora.TimeVault.Extensions.Common;
using Cencora.TimeVault.Models.Timezone;

namespace Cencora.TimeVault.Extensions.Timezone;

/// <summary>
/// The extensions for the <see cref="TimeZoneRequest"/> class and the <see cref="TimeZoneRequestDto"/> record.
/// </summary>
public static class TimeZoneRequestExtensions
{
    /// <summary>
    /// Converts a <see cref="TimeZoneRequestDto"/> object to a <see cref="TimeZoneRequest"/> object.
    /// </summary>
    /// <param name="dto">The <see cref="TimeZoneRequestDto"/> object to convert.</param>
    /// <returns>A new instance of <see cref="TimeZoneRequest"/> with the converted data.</returns>
    public static TimeZoneRequest ToModel(this TimeZoneRequestDto dto)
    {
        return new TimeZoneRequest
        {
            Address = dto.Address.ToModel()
        };
    }

    /// <summary>
    /// Converts a <see cref="TimeZoneRequest"/> object to a <see cref="TimeZoneRequestDto"/> object.
    /// </summary>
    /// <param name="request">The <see cref="TimeZoneRequest"/> object to convert.</param>
    /// <returns>A new instance of <see cref="TimeZoneRequestDto"/> with the converted data.</returns>
    public static TimeZoneRequestDto ToDto(this TimeZoneRequest request)
    {
        return new TimeZoneRequestDto
        {
            Address = request.Address.ToDto()
        };
    }
}