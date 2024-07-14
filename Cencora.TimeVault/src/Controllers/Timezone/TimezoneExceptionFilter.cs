// Copyright 2024 Cencora. All rights reserved.
//
// Written by Felix Kahle, A123234, felix.kahle@worldcourier.de

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Cencora.TimeVault.Controllers.Timezone;

/// <summary>
/// Exception filter for timezone controller.
/// </summary>
public class TimeZoneExceptionFilter : IExceptionFilter
{
    private readonly ILogger<TimeZoneExceptionFilter> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="TimeZoneExceptionFilter"/> class.
    /// </summary>
    /// <param name="logger">The logger.</param>
    public TimeZoneExceptionFilter(ILogger<TimeZoneExceptionFilter> logger)
    {
        _logger = logger;
    }

    /// <inheritdoc/>
    public void OnException(ExceptionContext context)
    {
        var exception = context.Exception;

        _logger.LogError(exception, "An unhandled exception occurred while processing the request.");

        var message = exception.Message;
        var statusCode = exception switch
        {
            TimeZoneNotFoundException => StatusCodes.Status404NotFound,
            _ => StatusCodes.Status500InternalServerError
        };

        ObjectResult result = new(new
        {
            Message = message,
            StatusCode = statusCode
        })
        {
            StatusCode = statusCode
        };

        context.Result = result;
    }
}