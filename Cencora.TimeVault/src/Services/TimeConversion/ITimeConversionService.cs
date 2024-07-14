// Copyright 2024 Cencora. All rights reserved.
//
// Written by Felix Kahle, A123234, felix.kahle@worldcourier.de

using Cencora.TimeVault.Models.TimeConversion;

namespace Cencora.TimeVault.Services.TimeConversion;

/// <summary>
/// The service for the time conversion.
/// </summary>
public interface ITimeConversionService
{
    /// <summary>
    /// Converts the given time to the given timezone.
    /// </summary>
    /// <param name="request">The request containing the time and the timezones.</param>
    /// <returns>The time conversion response.</returns>
    Task<TimeConversionResponse> ConvertTimeAsync(TimeConversionRequest request);
}