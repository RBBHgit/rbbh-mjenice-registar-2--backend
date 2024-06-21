namespace Raiffeisen.RegistarMjenica.Api.Services.DataModels;

public class ExcelDataExportModel
{
    public Dictionary<string, string> Headers { get; set; }
    public List<MjenicaGridModel> SelectedMjenice { get; set; }
}