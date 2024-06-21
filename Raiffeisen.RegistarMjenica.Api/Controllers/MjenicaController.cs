using Microsoft.AspNetCore.Mvc;
using Raiffeisen.RegistarMjenica.Api.Helpers;
using Raiffeisen.RegistarMjenica.Api.Models;
using Raiffeisen.RegistarMjenica.Api.Services.DataModels;
using Raiffeisen.RegistarMjenica.Api.Services.Interfaces;

namespace Raiffeisen.RegistarMjenica.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MjenicaController : ControllerBase
{
    private readonly IMjenicaJobService _mjenicaJobService;
    private readonly IMjenicaService _mjenicaService;


    public MjenicaController(IMjenicaJobService mjenicaJobService, IMjenicaService mjenicaService)
    {
        _mjenicaJobService = mjenicaJobService;
        _mjenicaService = mjenicaService;
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
            return StatusCode(500, "Internal server error: " + ex.Message);
        }
    }

    [HttpPut("Verify")]
    public async Task<IActionResult> Verify([FromBody] MjenicaApiRequest mjenicaApiRequest)
    {
        try
        {
            var result = await _mjenicaService.VerifyMjenicaAsync(mjenicaApiRequest.Mjenica, mjenicaApiRequest.LoggedInUser);
            return Ok(result);
        }
        catch (Exception ex)
        {
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
            return StatusCode(500, "Internal server error: " + ex.Message);
        }
    }

    [HttpGet("GetClientMjenice")]
    public async Task<IActionResult> GetClientMjenice([FromQuery] string id)
    {
        if (string.IsNullOrEmpty(id)) return BadRequest("Invalid request payload.");

        try
        {
            var result = await _mjenicaService.GetClientMjeniceAsync(id);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal server error: " + ex.Message);
        }
    }
}