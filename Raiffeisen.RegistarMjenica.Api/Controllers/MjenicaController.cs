using System.Text;
using Microsoft.AspNetCore.Mvc;
using Raiffeisen.RegistarMjenica.Api.Helpers;
using Raiffeisen.RegistarMjenica.Api.Models;
using Raiffeisen.RegistarMjenica.Api.Services.DataModels;
using Raiffeisen.RegistarMjenica.Api.Services.DataModels.SearchObjects;
using Raiffeisen.RegistarMjenica.Api.Services.Exceptions;
using Raiffeisen.RegistarMjenica.Api.Services.Interfaces;

namespace Raiffeisen.RegistarMjenica.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MjenicaController : ControllerBase
{
    private readonly IMjenicaJobService _mjenicaJobService;
    private readonly IMjenicaService _mjenicaService;
    private readonly IRfLogger _logger;


    public MjenicaController(IMjenicaJobService mjenicaJobService, IMjenicaService mjenicaService, IRfLogger logger)
    {
        _mjenicaJobService = mjenicaJobService;
        _mjenicaService = mjenicaService;
        _logger = logger;
    }

    [HttpPost("UpdateContractStatusBatch")]
    public async Task<IActionResult> UpdateContractStatusBatch(
        [FromBody] Dictionary<string, string> contractsStatusBatch)
    {
        var result = await _mjenicaJobService.UpdateContractStatus(contractsStatusBatch);

        return GenericIActionResult<int, MjenicaJobResult>.CreateStatusCodeResult(result.StatusCode, result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetMjenica(int id)
    {
        try
        {
            var result = await _mjenicaService.GetByIdAsync(id, false, true);

            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occured while fetching mjenica");
            return StatusCode(500, "Internal server error: " + ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] MjenicaApiRequest mjenicaApiRequest)
    {
        try
        {
            var result = await _mjenicaService.CreateAsync(mjenicaApiRequest.Mjenica, mjenicaApiRequest.LoggedInUser);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occured while creating mjenica");
            return StatusCode(500, "Internal server error: " + ex.Message);
        }
    }

    [HttpPut("Verify")]
    public async Task<IActionResult> Verify([FromBody] MjenicaApiRequest mjenicaApiRequest)
    {
        try
        {
            var result =
                await _mjenicaService.VerifyMjenicaAsync(mjenicaApiRequest.Mjenica, mjenicaApiRequest.LoggedInUser);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occured while verifying mjenica");
            return StatusCode(500, "Internal server error: " + ex.Message);
        }
    }

    [HttpPut("Update")]
    public async Task<IActionResult> Update([FromBody] MjenicaApiRequest mjenicaApiRequest)
    {
        try
        {
            var result = await _mjenicaService.UpdateAsync(mjenicaApiRequest.Mjenica, mjenicaApiRequest.LoggedInUser);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occured while updating mjenica");
            return StatusCode(500, "Internal server error: " + ex.Message);
        }
    }

    [HttpPut("ExemptTransaction")]
    public async Task<IActionResult> ExemptMjenicaTransaction([FromBody] MjenicaApiRequest mjenicaApiRequest)
    {
        try
        {
            var result =
                await _mjenicaService.ExemptMjenicaTransactionAsync(mjenicaApiRequest.Mjenica,
                    mjenicaApiRequest.LoggedInUser);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while processing the mjenica exemption transaction");
            return StatusCode(500, "Internal server error: " + ex.Message);
        }
    }

    [HttpPut("VerifyExemption")]
    public async Task<IActionResult> VerifyMjenicaExemption([FromBody] MjenicaApiRequest mjenicaApiRequest)
    {
        try
        {
            var result =
                await _mjenicaService.VerifyExemptionMjenicaAsync(mjenicaApiRequest.Mjenica,
                    mjenicaApiRequest.LoggedInUser);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occured while verifying exemption");
            return StatusCode(500, "Internal server error: " + ex.Message);
        }
    }

    [HttpGet("GetClientMjenice")]
    public async Task<IActionResult> GetClientMjenice([FromQuery] string id)
    {
        try
        {
            var result = await _mjenicaService.GetClientMjeniceAsync(id);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occured while getting client mjenice");
            return StatusCode(500, "Internal server error: " + ex.Message);
        }
    }

    [HttpGet("ApplyFilter")]
    public async Task<IActionResult> ApplyFilter([FromQuery] MjenicaSearchObject searchObject)
    {
        try
        {
            var result = await _mjenicaService.GetGridAsync(searchObject);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occured while applying filter on mjenica");
            return StatusCode(500, "Internal server error: " + ex.Message);
        }
    }
}