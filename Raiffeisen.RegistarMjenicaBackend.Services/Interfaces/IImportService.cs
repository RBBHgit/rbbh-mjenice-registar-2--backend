using Microsoft.AspNetCore.Components.Forms;

namespace Raiffeisen.RegistarMjenicaBackend.Services.Interfaces;

public interface IImportService
{
    Task<int> ImportFromExcel(IBrowserFile file);
}