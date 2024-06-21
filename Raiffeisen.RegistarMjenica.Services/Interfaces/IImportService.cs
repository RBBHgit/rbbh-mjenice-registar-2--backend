using Microsoft.AspNetCore.Components.Forms;

namespace Raiffeisen.RegistarMjenica.Api.Services.Interfaces;

public interface IImportService
{
    Task<int> ImportFromExcel(IBrowserFile file);
}