// Copyright 2024 Cencora. All rights reserved.
//
// Written by Felix Kahle, A123234, felix.kahle@worldcourier.de

using Cencora.TimeVault.Models.Timezone;

namespace Cencora.TimeVault.Services.Timezone;

/// <summary>
/// The service for the timezone.
/// </summary>
public interface ITimeZoneService
{
    /// <summary>
    /// Gets the timezone for the given address.
    /// </summary>
    /// <param name="request">The request containing the address.</param>
    /// <returns>The timezone response.</returns>
    Task<TimezoneResponse> GetTimezoneAsync(TimeZoneRequest request);
}