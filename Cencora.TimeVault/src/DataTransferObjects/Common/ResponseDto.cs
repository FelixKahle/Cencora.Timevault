// Copyright 2024 Cencora. All rights reserved.
//
// Written by Felix Kahle, A123234, felix.kahle@worldcourier.de

using System.Net;
using System.Text.Json.Serialization;

namespace Cencora.TimeVault.DataTransferObjects.Common;

/// <summary>
/// Represents a response DTO.
/// </summary>
public abstract record ResponseDto
{
    /// <summary>
    /// Gets or sets the error details.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ErrorDetailDto? Error { get; set; }

    /// <summary>
    /// Gets or sets the status code.
    /// </summary>
    [JsonInclude]
    public required int StatusCode { get; set; } = (int)HttpStatusCode.OK;
}