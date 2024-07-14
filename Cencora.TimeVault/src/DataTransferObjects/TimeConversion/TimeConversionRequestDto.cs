// Copyright 2024 Cencora. All rights reserved.
//
// Written by Felix Kahle, A123234, felix.kahle@worldcourier.de

using System.ComponentModel.DataAnnotations;

namespace Cencora.TimeVault.DataTransferObjects.TimeConversion;

/// <summary>
/// Represents a request for a time conversion.
/// </summary>
public record TimeConversionRequestDto : IValidatableObject
{
    /// <summary>
    /// Gets or sets the origin timezone.
    /// </summary>
    [Required]
    public required string OriginTimeZoneId { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the destination timezone.
    /// </summary>
    [Required]
    public required string DestinationTimeZoneId { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the time.
    /// </summary>
    [Required]
    public required string Time { get; init; } = string.Empty;

    /// <inheritdoc />
    public override string ToString()
    {
        return $"{Time} {OriginTimeZoneId} -> {DestinationTimeZoneId}";
    }

    /// <inheritdoc />
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (string.IsNullOrWhiteSpace(OriginTimeZoneId))
        {
            yield return new ValidationResult("The origin timezone is required.", [nameof(OriginTimeZoneId)]);
        }

        if (string.IsNullOrWhiteSpace(DestinationTimeZoneId))
        {
            yield return new ValidationResult("The destination timezone is required.", [nameof(DestinationTimeZoneId)]);
        }

        if (string.IsNullOrWhiteSpace(Time))
        {
            yield return new ValidationResult("The time is required.", [nameof(Time)]);
        }

        if (TimeZoneInfo.TryFindSystemTimeZoneById(OriginTimeZoneId, out _) == false)
        {
            yield return new ValidationResult("The origin timezone is invalid.", [nameof(OriginTimeZoneId)]);
        }

        if (TimeZoneInfo.TryFindSystemTimeZoneById(DestinationTimeZoneId, out _) == false)
        {
            yield return new ValidationResult("The destination timezone is invalid.", [nameof(DestinationTimeZoneId)]);
        }

        if (DateTime.TryParse(Time, out _) == false)
        {
            yield return new ValidationResult("The time is invalid.", [nameof(Time)]);
        }
    }
}