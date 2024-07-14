// Copyright 2024 Cencora. All rights reserved.
//
// Written by Felix Kahle, A123234, felix.kahle@worldcourier.de

using Cencora.Common.Maps;
using Cencora.TimeVault.Models.Common;

namespace Cencora.TimeVault.Models.Timezone;

/// <summary>
/// Represents a response containing timezone information.
/// </summary>
public class TimezoneResponse : Response
{
    /// <summary>
    /// Gets or sets the timezone information.
    /// </summary>
    public TimeZoneInfo? TimeZone { get; init; }

    /// <summary>
    /// Gets or sets the address associated with the timezone.
    /// </summary>
    public required Address Address { get; init; }

    /// <summary>
    /// Gets or sets the date and time when the request was made.
    /// </summary>
    public DateTimeOffset RequestedAt { get; init; } = DateTimeOffset.UtcNow;

    /// <inheritdoc/>
    public override string ToString()
    {
        var timezoneId = TimeZone?.Id ?? "Unknown";
        return $"{Address}: {timezoneId}";
    }
}