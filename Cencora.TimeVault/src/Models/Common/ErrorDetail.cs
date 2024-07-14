// Copyright 2024 Cencora. All rights reserved.
//
// Written by Felix Kahle, A123234, felix.kahle@worldcourier.de

namespace Cencora.TimeVault.Models.Common;

/// <summary>
/// Represents an error detail object.
/// </summary>
public class ErrorDetail
{
    /// <summary>
    /// Gets or sets the error message.
    /// </summary>
    public required string? Message { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the error code.
    /// </summary>
    public required string? ErrorCode { get; init; } = string.Empty;

    /// <inheritdoc/>
    public override string ToString()
    {
        return $"{ErrorCode} | {Message}";
    }
}