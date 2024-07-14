// Copyright 2024 Cencora. All rights reserved.
//
// Written by Felix Kahle, A123234, felix.kahle@worldcourier.de

namespace Cencora.TimeVault.Models.TimeConversion;

/// <summary>
/// Represents a request for a time conversion.
/// </summary>
public class TimeConversionRequest
{
    // We do not need to use the nullable reference types here, 
    // because incoming requests are getting validated by the controller.
    private readonly TimeZoneInfo _originTimeZone = TimeZoneInfo.Utc;
    private readonly TimeZoneInfo _destinationTimeZone = TimeZoneInfo.Utc;

    /// <summary>
    /// Gets or sets the time to convert.
    /// </summary>
    public required DateTime Time { get; init; }
        
    /// <summary>
    /// Gets or sets the origin timezone.
    /// </summary>
    /// <exception cref="ArgumentNullException">Thrown when the value is null.</exception>
    public required TimeZoneInfo OriginTimeZone
    {
        get => _originTimeZone;
        init => _originTimeZone = value ?? throw new ArgumentNullException(nameof(value));
    }

    /// <summary>
    /// Gets or sets the destination timezone.
    /// </summary>
    /// <exception cref="ArgumentNullException">Thrown when the value is null.</exception>
    public required TimeZoneInfo DestinationTimeZone
    {
        get => _destinationTimeZone;
        init => _destinationTimeZone = value ?? throw new ArgumentNullException(nameof(value));
    }

    /// <inheritdoc/>
    public override string ToString()
    {
        return $"{Time} {OriginTimeZone.Id} -> {DestinationTimeZone.Id}";
    }
}