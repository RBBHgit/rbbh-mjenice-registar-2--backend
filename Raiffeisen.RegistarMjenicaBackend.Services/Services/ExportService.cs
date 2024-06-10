using OfficeOpenXml;
using Raiffeisen.RegistarMjenicaBackend.Services.Interfaces;

namespace Raiffeisen.RegistarMjenicaBackend.Services.Services;

public class ExportService : IExportService
{
    public byte[] ConvertToExcel<T>(IQueryable<T> data, Dictionary<string, string> headers)
    {
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        var headerMapper = new Dictionary<string, int>();

        using var package = new ExcelPackage();
        var worksheet = package.Workbook.Worksheets.Add("RegistarMjenica");
        var properties = typeof(T).GetProperties();

        for (var i = 1; i <= headers.Count; i++)
        {
            var header = headers.ElementAt(i - 1);
            worksheet.Cells[1, i].Value = header.Value;
            worksheet.Cells[1, i].Style.Font.Bold = true;
            headerMapper[header.Key] = i;
        }

        var rowIndex = 2;
        foreach (var item in data)
        {
            foreach (var prop in properties)
                if (headerMapper.TryGetValue(prop.Name, out var columnIndex))
                {
                    var cellValue = prop.GetValue(item, null);
                    worksheet.Cells[rowIndex, columnIndex].Value = cellValue is DateTime ? cellValue.ToString() : cellValue;
                }
            rowIndex++;
        }

        worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

        return package.GetAsByteArray();
    }

    public string GetExcelURI<T>(IQueryable<T> data, Dictionary<string, string> headers)
    {
        var excelBytes = ConvertToExcel(data, headers);
        
        var mimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        //var contentDisposition = $"attachment; filename={fileName}";

        return $"data:{mimeType};base64,{Convert.ToBase64String(excelBytes)}";
    }
}