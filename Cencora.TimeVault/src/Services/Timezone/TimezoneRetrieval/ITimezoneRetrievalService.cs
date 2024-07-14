// Copyright 2024 Cencora. All rights reserved.
//
// Written by Felix Kahle, A123234, felix.kahle@worldcourier.de

using Cencora.Common.Api;
using Cencora.Common.Maps;

namespace Cencora.TimeVault.Services.Timezone.TimezoneRetrieval;

/// <summary>
/// The service for the timezone.
/// </summary>
public interface ITimezoneRetrievalService
{
    /// <summary>
    /// Gets the timezone for the specified address.
    /// </summary>
    /// <param name="address">The address to get the timezone for.</param>
    /// <returns>The timezone for the specified address.</returns>
    Task<ApiResponse<TimeZoneInfo>> GetTimezoneAsync(Address address);

    /// <summary>
    /// Gets the timezones for the specified addresses.
    /// </summary>
    /// <param name="addresses">The addresses to get the timezones for.</param>
    /// <returns>The timezones for the specified addresses.</returns>
    Task<List<ApiResponse<TimeZoneInfo>>> GetTimezonesBatchAsync(List<Address> addresses);
}