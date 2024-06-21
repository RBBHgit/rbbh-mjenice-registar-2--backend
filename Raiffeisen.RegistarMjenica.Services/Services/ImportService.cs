using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Components.Forms;
using OfficeOpenXml;
using Raiffeisen.RegistarMjenica.Api.Services.DataModels;
using Raiffeisen.RegistarMjenica.Api.Services.Exceptions;
using Raiffeisen.RegistarMjenica.Api.Services.Interfaces;

namespace Raiffeisen.RegistarMjenica.Api.Services.Services;

public class ImportService : IImportService
{
    private static readonly Dictionary<string, Action<MjenicaModel, string>> ColumnMappings =
        new(StringComparer.OrdinalIgnoreCase)
        {
            { "KOMITENT", (obj, cellValue) => obj.ClientName = cellValue },
            { "BROJ PARTIJE", (obj, cellValue) => obj.GroupNumber = cellValue },
            { "DAT. UGOVORA", (obj, cellValue) => obj.ContractDate = GetDate(cellValue) },
            { "SERIJSKI BROJ MJENICA", (obj, cellValue) => GetMjenicaSerialNumbers(obj, cellValue) },
            { "DATUM DOSPJ", (obj, cellValue) => obj.CreatedDate = GetDate(cellValue) }
        };

    private readonly IRfLogger _logger;
    private readonly IMjenicaService _mjenicaService;

    public ImportService(IMjenicaService mjenicaService, IRfLogger logger)
    {
        _mjenicaService = mjenicaService;
        _logger = logger;
    }

    public async Task<int> ImportFromExcel(IBrowserFile file)
    {
        try
        {
            var data = await ReadFromFile(file);

            var rowsCount = await _mjenicaService.CreateBatchAsync(data);

            return rowsCount;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error importing data from excel file");
            throw;
        }
    }

    private async Task<List<MjenicaModel>> ReadFromFile(IBrowserFile file)
    {
        var stream = await ConvertToSeekableStream(file);

        var data = new List<MjenicaModel>();

        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;


        using var package = new ExcelPackage(stream);
        var worksheet = package.Workbook.Worksheets[0];

        for (var row = 2; row <= worksheet.Dimension.Rows; row++)
        {
            var mjenica = new MjenicaModel();

            for (var col = 1; col <= worksheet.Dimension.Columns; col++)
            {
                var columnName = worksheet.Cells[1, col].Text;
                var cellValue = worksheet.Cells[row, col].Text;
                var cellComment = worksheet.Cells[row, col].Comment?.Text;

                MapCellValueToProperty(mjenica, columnName, cellValue);

                if (!string.IsNullOrWhiteSpace(cellComment))
                    mjenica.FreeTextField = string.Concat(mjenica.FreeTextField, cellComment, "\n");
            }

            data.Add(mjenica);
        }

        return data;
    }

    private async Task<Stream> ConvertToSeekableStream(IBrowserFile file)
    {
        await using var nonSeekableStream = file.OpenReadStream();
        var seekableStream = new MemoryStream();
        await nonSeekableStream.CopyToAsync(seekableStream);
        seekableStream.Seek(0, SeekOrigin.Begin);
        return seekableStream;
    }

    private void MapCellValueToProperty(MjenicaModel obj, string columnName, string cellValue)
    {
        if (ColumnMappings.TryGetValue(columnName.Trim(), out var action)) action(obj, cellValue);
    }

    private static void GetMjenicaSerialNumbers(MjenicaModel obj, string value)
    {
        try
        {
            var pattern = @"\b[A-Z]{2}\s*\d+\b";
            var regex = new Regex(pattern);

            var matches = regex.Matches(value);

            obj.ClientMjenicaSerialNumber = matches.Count > 0 ? matches[0].Value : string.Empty;
            obj.GuarantorMjenicaSerialNumber = matches.Count > 1 ? matches[1].Value : string.Empty;
        }
        catch (Exception ex)
        {
            // ignored
        }
    }

    private static DateTime GetDate(string cellValue)
    {
        try
        {
            return !string.IsNullOrEmpty(cellValue) ? DateTime.Parse(cellValue) : DateTime.Now;
        }
        catch (Exception ex)
        {
            return DateTime.Now;
        }
    }
}