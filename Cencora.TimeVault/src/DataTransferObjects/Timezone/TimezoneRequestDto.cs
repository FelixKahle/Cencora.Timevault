// Copyright 2024 Cencora. All rights reserved.
//
// Written by Felix Kahle, A123234, felix.kahle@worldcourier.de

using System.ComponentModel.DataAnnotations;
using Cencora.TimeVault.DataTransferObjects.Common;

namespace Cencora.TimeVault.DataTransferObjects.Timezone;

/// <summary>
/// The request to get the timezone for an address.
/// </summary>
public record TimeZoneRequestDto : IValidatableObject
{
    /// <summary>
    /// The address to get the timezone for.
    /// </summary>
    [Required]
    public required AddressDto Address { get; init; }
        
    /// <inheritdoc/>
    public override string ToString()
    {
        return $"{Address}";
    }

    /// <inheritdoc/>
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (Address.IsEmpty)
        {
            yield return new ValidationResult("The address must not be empty.", [nameof(Address)]);
        }
    }
}