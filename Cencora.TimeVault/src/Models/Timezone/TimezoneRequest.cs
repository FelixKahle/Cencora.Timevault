// Copyright 2024 Cencora. All rights reserved.
//
// Written by Felix Kahle, A123234, felix.kahle@worldcourier.de

using Cencora.Common.Maps;
using Cencora.TimeVault.Extensions.Common;

namespace Cencora.TimeVault.Models.Timezone;

/// <summary>
/// The request to get the timezone for an address.
/// </summary>
public class TimeZoneRequest
{
    /// <summary>
    /// The address to get the timezone for.
    /// </summary>
    public required Address Address { get; init; }

    /// <inheritdoc/>
    public override string ToString()
    {
        return $"{Address.ToFormattedString()}";
    }
}