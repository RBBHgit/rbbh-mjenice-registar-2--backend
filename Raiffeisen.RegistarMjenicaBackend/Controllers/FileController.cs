using Microsoft.AspNetCore.Mvc;
using Raiffeisen.RegistarMjenicaBackend.Helpers;
using Raiffeisen.RegistarMjenicaBackend.Services.DataModels;
using Raiffeisen.RegistarMjenicaBackend.Services.Interfaces;


namespace Raiffeisen.RegistarMjenicaBackend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FileController : ControllerBase
{
    private readonly IExportService _exportService;

    public FileController(IExportService service)
    {
        _exportService = service;
    }

    [HttpPost]
    public IActionResult GetExcelURI([FromBody] ExcelDataExportModel model)
    {
        var excelFileBytes = _exportService.GetExcelURI(model.SelectedMjenice.AsQueryable(), model.Headers);

        if (excelFileBytes == null)
            return GenericIActionResult<int, ExcelDataExportModel>.CreateStatusCodeResult(500, model);

        return GenericIActionResult<int, string>.CreateStatusCodeResult(200, excelFileBytes);
    }
}