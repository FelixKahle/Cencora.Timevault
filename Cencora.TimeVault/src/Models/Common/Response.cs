// Copyright 2024 Cencora. All rights reserved.
//
// Written by Felix Kahle, A123234, felix.kahle@worldcourier.de

using System.Net;

namespace Cencora.TimeVault.Models.Common;

/// <summary>
/// Represents a response object.
/// </summary>
public abstract class Response
{
    /// <summary>
    /// Gets or sets the error details.
    /// </summary>
    // ReSharper disable once MemberCanBePrivate.Global
    public ErrorDetail? Error = null;

    /// <summary>
    /// Gets or sets the status code.
    /// </summary>
    public required int StatusCode { get; init; } = (int)HttpStatusCode.OK;

    /// <summary>
    /// Gets a value indicating whether the response is an error.
    /// </summary>
    public bool HasError => Error != null;
}