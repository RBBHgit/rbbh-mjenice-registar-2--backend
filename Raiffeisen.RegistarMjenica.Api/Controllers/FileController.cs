using Microsoft.AspNetCore.Mvc;
using Raiffeisen.RegistarMjenica.Api.Helpers;
using Raiffeisen.RegistarMjenica.Api.Services.DataModels;
using Raiffeisen.RegistarMjenica.Api.Services.Exceptions;
using Raiffeisen.RegistarMjenica.Api.Services.Interfaces;

namespace Raiffeisen.RegistarMjenica.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FileController : ControllerBase
{
    private readonly IExportService _exportService;
    private readonly IRfLogger _logger;
    private readonly IImportService _importService;

    public FileController(IExportService service, IRfLogger logger, IImportService importService)
    {
        _exportService = service;
        _logger = logger;
        _importService = importService;
    }

    [HttpPost]
    public IActionResult GetExcelURI([FromBody] ExcelDataExportModel model)
    {
        try
        {
            var excelFileBytes = _exportService.GetExcelURI(model.SelectedMjenice.AsQueryable(), model.Headers);
            return Ok(excelFileBytes);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error exporting excel file");
            return StatusCode(500, "Internal server error: " + ex.Message);
        }
    }

    [HttpPost("ImportExcelFile")]
    public async Task<IActionResult> ImportExcelFile([FromForm] IFormFile file)
    {
        try
        {
            var rowsCount = await _importService.ImportFromExcel(file.OpenReadStream());

            return Ok(new { RowCount = rowsCount });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error uploading file");
            return StatusCode(500, "Internal server error: " + ex.Message);
        }
    }
}