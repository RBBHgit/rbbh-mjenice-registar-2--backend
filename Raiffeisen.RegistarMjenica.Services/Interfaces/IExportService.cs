namespace Raiffeisen.RegistarMjenica.Api.Services.Interfaces;

public interface IExportService
{
    public byte[] ConvertToExcel<T>(IQueryable<T> data, Dictionary<string, string> headers);

    public string GetExcelURI<T>(IQueryable<T> data, Dictionary<string, string> headers);
}