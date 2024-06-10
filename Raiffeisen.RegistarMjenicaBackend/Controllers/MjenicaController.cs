using Microsoft.AspNetCore.Mvc;
using Raiffeisen.RegistarMjenicaBackend.Helpers;
using Raiffeisen.RegistarMjenicaBackend.Services.DataModels;
using Raiffeisen.RegistarMjenicaBackend.Services.Interfaces;

namespace Raiffeisen.RegistarMjenica.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MjenicaController : ControllerBase
{
    private readonly IMjenicaJobService _mjenicaJobService;

    public MjenicaController(IMjenicaJobService mjenicaJobService)
    {
        _mjenicaJobService = mjenicaJobService;
    }

    // POST api/<MjenicaController>
    /// <summary>
    ///     Method updates Mjenica contract status. Key is ContractNumber and Value is ContractState
    /// </summary>
    /// <param name="contractsStatusBatch"></param>
    /// <returns></returns>
    [HttpPost("UpdateContractStatusBatch")]
    public async Task<IActionResult> UpdateContractStatusBatch(
        [FromBody] Dictionary<string, string> contractsStatusBatch)
    {
        var result = await _mjenicaJobService.UpdateContractStatus(contractsStatusBatch);
        
        return GenericIActionResult<int, MjenicaJobResult>.CreateStatusCodeResult(result.StatusCode, result);
    }

   
}