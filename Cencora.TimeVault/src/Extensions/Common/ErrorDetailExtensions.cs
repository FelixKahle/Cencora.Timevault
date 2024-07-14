// Copyright 2024 Cencora. All rights reserved.
//
// Written by Felix Kahle, A123234, felix.kahle@worldcourier.de

using Cencora.TimeVault.DataTransferObjects.Common;
using Cencora.TimeVault.Models.Common;

namespace Cencora.TimeVault.Extensions.Common;

/// <summary>
/// Extensions for the <see cref="ErrorDetail"/> class and <see cref="ErrorDetailDto"/> record.
/// </summary>
public static class ErrorDetailExtensions
{
    /// <summary>
    /// Converts the <see cref="ErrorDetailDto"/> record to a <see cref="ErrorDetail"/> class.
    /// </summary>
    /// <param name="dto">The <see cref="ErrorDetailDto"/> record to convert.</param>
    /// <returns>The <see cref="ErrorDetail"/> class.</returns>
    public static ErrorDetail ToModel(this ErrorDetailDto dto)
    {
        return new ErrorDetail
        {
            Message = dto.Message,
            ErrorCode = dto.ErrorCode
        };
    }

    /// <summary>
    /// Converts the <see cref="ErrorDetail"/> class to a <see cref="ErrorDetailDto"/> record.
    /// </summary>
    /// <param name="model">The <see cref="ErrorDetail"/> class to convert.</param>
    /// <returns>The <see cref="ErrorDetailDto"/> record.</returns>
    public static ErrorDetailDto ToDto(this ErrorDetail model)
    {
        return new ErrorDetailDto
        {
            Message = model.Message ?? string.Empty,
            ErrorCode = model.ErrorCode ?? string.Empty
        };
    }
}