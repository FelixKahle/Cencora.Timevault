// Copyright 2024 Cencora. All rights reserved.
//
// Written by Felix Kahle, A123234, felix.kahle@worldcourier.de

using Cencora.TimeVault.DataTransferObjects.Timezone;
using Cencora.TimeVault.Extensions.Timezone;
using Cencora.TimeVault.Services.Timezone;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;

namespace Cencora.TimeVault.Controllers.Timezone;

/// <summary>
/// The controller for the timezone.
/// </summary>
[Authorize]
[ApiController]
[Route("api/v1/timezone")]
public class TimeZoneController : ControllerBase
{
    private readonly ILogger<TimeZoneController> _logger;
    private readonly ITimeZoneService _timezoneService;

    /// <summary>
    /// Initializes a new instance of the <see cref="TimeZoneController"/> class.
    /// </summary>
    public TimeZoneController(ILogger<TimeZoneController> logger, ITimeZoneService timezoneService)
    {
        _logger = logger;
        _timezoneService = timezoneService;
    }
    
    [HttpGet]
    [Route("status")]
    [RequiredScope("TimeZone.Status.Read")]
    public ActionResult<string> Status()
    {
        return "OK";
    }

    /// <summary>
    /// Gets the timezone for the given request.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>The timezone response.</returns>
    [HttpGet]
    [TypeFilter(typeof(TimeZoneExceptionFilter))]
    public async Task<ActionResult<TimeZoneResponseDto>> GetTimezone([FromQuery] TimeZoneRequestDto request)
    {
        _logger.LogInformation("Received get timezone request.");

        var timezoneRequest = request.ToModel();
        var timezoneResponse = await _timezoneService.GetTimezoneAsync(timezoneRequest);
        return Ok(timezoneResponse.ToDto());
    }

    /// <summary>
    /// Posts the timezone for the given request.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>The timezone response.</returns>
    [HttpPost]
    [TypeFilter(typeof(TimeZoneExceptionFilter))]
    public async Task<ActionResult<TimeZoneResponseDto>> PostTimezone([FromBody] TimeZoneRequestDto request)
    {
        _logger.LogInformation("Received post timezone request.");
            
        var timezoneRequest = request.ToModel();
        var timezoneResponse = await _timezoneService.GetTimezoneAsync(timezoneRequest);
        return Ok(timezoneResponse.ToDto());
    }
}