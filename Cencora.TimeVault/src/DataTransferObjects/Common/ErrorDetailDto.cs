// Copyright 2024 Cencora. All rights reserved.
//
// Written by Felix Kahle, A123234, felix.kahle@worldcourier.de

using System.Text.Json.Serialization;
using Cencora.TimeVault.Utils;

namespace Cencora.TimeVault.DataTransferObjects.Common;

/// <summary>
/// Represents an error detail DTO that provides information about an error.
/// </summary>
public record ErrorDetailDto
{
    /// <summary>
    /// Gets or sets the error message.
    /// </summary>
    [JsonConverter(typeof(IgnoreNullOrWhiteSpaceStringConverter))]
    public required string Message { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the error code.
    /// </summary>
    [JsonConverter(typeof(IgnoreNullOrWhiteSpaceStringConverter))]
    public required string ErrorCode { get; init; } = string.Empty;

    /// <inheritdoc/>
    public override string ToString()
    {
        return $"{ErrorCode} | {Message}";
    }
}