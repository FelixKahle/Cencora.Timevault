// Copyright 2024 Cencora. All rights reserved.
//
// Written by Felix Kahle, A123234, felix.kahle@worldcourier.de

using Cencora.TimeVault.Models.Common;

namespace Cencora.TimeVault.Models.TimeConversion;

/// <summary>
/// Represents a response for a time conversion.
/// </summary>
public class TimeConversionResponse : Response
{
    private readonly TimeZoneInfo? _originTimeZone;
    private readonly TimeZoneInfo? _destinationTimeZone;

    /// <summary>
    /// Gets or sets the converted time.
    /// </summary>
    public required DateTime? ConvertedTime { get; init; }

    /// <summary>
    /// Gets or sets the original time.
    /// </summary>
    public required DateTime? OriginalTime { get; init; }

    /// <summary>
    /// Gets or sets the time when the request was made.
    /// </summary>
    public DateTimeOffset RequestedAt { get; init; } = DateTimeOffset.UtcNow;

    /// <summary>
    /// Gets or sets the origin timezone.
    /// </summary>
    /// <exception cref="ArgumentNullException">Thrown when the value is null.</exception>
    public required TimeZoneInfo? OriginTimeZone
    {
        get => _originTimeZone;
        init => _originTimeZone = value ?? throw new ArgumentNullException(nameof(value));
    }

    /// <summary>
    /// Gets or sets the destination timezone.
    /// </summary>
    /// <exception cref="ArgumentNullException">Thrown when the value is null.</exception>
    public required TimeZoneInfo? DestinationTimeZone
    {
        get => _destinationTimeZone;
        init => _destinationTimeZone = value ?? throw new ArgumentNullException(nameof(value));
    }

    /// <inheritdoc/>
    public override string ToString()
    {
        var originTimeZoneId = OriginTimeZone?.Id ?? "null";
        var destinationTimeZoneId = DestinationTimeZone?.Id ?? "null";
            
        return $"{OriginalTime} {originTimeZoneId} -> {ConvertedTime} {destinationTimeZoneId}";
    }
}