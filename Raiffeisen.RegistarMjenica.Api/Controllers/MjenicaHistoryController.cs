using Microsoft.AspNetCore.Mvc;
using Raiffeisen.RegistarMjenica.Api.Services.DataModels.SearchObjects;
using Raiffeisen.RegistarMjenica.Api.Services.Exceptions;
using Raiffeisen.RegistarMjenica.Api.Services.Interfaces;

namespace Raiffeisen.RegistarMjenica.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MjenicaHistoryController : ControllerBase
{
    private readonly IMjenicaExemptionHistoryService _mjenicaExemptionHistoryService;
    private readonly IRfLogger _logger;

    public MjenicaHistoryController(IRfLogger logger, IMjenicaExemptionHistoryService mjenicaExemptionHistoryService)
    {
        _logger = logger;
        _mjenicaExemptionHistoryService = mjenicaExemptionHistoryService;
    }


    [HttpGet]
    public async Task<IActionResult> GetMjenicaHistory([FromQuery] MjenicaHistorySearchObject search)
    {
        try
        {
            var result = await _mjenicaExemptionHistoryService.Get(search);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occured while fetching mjenica history");
            return StatusCode(500, "Internal server error");
        }
    }
}