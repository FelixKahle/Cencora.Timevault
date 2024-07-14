// Copyright 2024 Cencora. All rights reserved.
//
// Written by Felix Kahle, A123234, felix.kahle@worldcourier.de

using Cencora.Common.Api;
using Cencora.Common.Maps;

namespace Cencora.TimeVault.Services.Timezone.TimezoneRetrieval.Azure;

/// <summary>
/// Uses the Azure Maps API to retrieve timezones.
/// </summary>
public class AzureTimezoneRetrievalService : ITimezoneRetrievalService
{
    /// <inheritdoc/>
    public Task<ApiResponse<TimeZoneInfo>> GetTimezoneAsync(Address address)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public Task<List<ApiResponse<TimeZoneInfo>>> GetTimezonesBatchAsync(List<Address> addresses)
    {
        throw new NotImplementedException();
    }
}