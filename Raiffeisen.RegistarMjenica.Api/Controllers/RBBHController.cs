using Microsoft.AspNetCore.Mvc;
using Raiffeisen.RegistarMjenica.Api.Services.DataModels;
using Raiffeisen.RegistarMjenica.Api.Services.Exceptions;
using Raiffeisen.RegistarMjenica.Api.Services.Interfaces;

namespace Raiffeisen.RegistarMjenica.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RBBHController : ControllerBase
{
    private readonly IRfLogger _logger;
    private readonly IRBBHApiService _rbbhApiService;
    
    public RBBHController(IRfLogger logger, IRBBHApiService rbbhApiService)
    {
        _logger = logger;
        _rbbhApiService = rbbhApiService;
    }

    [HttpGet("GetClientAgreements")]
    public async Task<List<Agreement>> GetClientAgreements([FromQuery]string customerId)
    {
        try
        {
            var result = await _rbbhApiService.GetClientAgreements(customerId);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,"An error occured while fetching client agreements");
            throw;
        }
       
    }
}