// Copyright 2024 Cencora. All rights reserved.
//
// Written by Felix Kahle, A123234, felix.kahle@worldcourier.de

using System.Net;
using Cencora.TimeVault.Models.TimeConversion;

namespace Cencora.TimeVault.Services.TimeConversion;

/// <summary>
/// The service for the time conversion.
/// </summary>
public class TimeConversionService : ITimeConversionService
{
    /// <inheritdoc/>
    public Task<TimeConversionResponse> ConvertTimeAsync(TimeConversionRequest request)
    {
        // Store the time when the request was made
        var requestedAt = DateTimeOffset.UtcNow;

        var time = request.Time;
        var originTimeZone = request.OriginTimeZone;
        var destinationTimeZone = request.DestinationTimeZone;

        var convertedTime = TimeZoneInfo.ConvertTime(time, originTimeZone, destinationTimeZone);

        return Task.FromResult(new TimeConversionResponse
        {
            StatusCode = (int)HttpStatusCode.OK,
            OriginalTime = time,
            ConvertedTime = convertedTime,
            OriginTimeZone = originTimeZone,
            DestinationTimeZone = destinationTimeZone,
            RequestedAt = requestedAt
        });
    }
}