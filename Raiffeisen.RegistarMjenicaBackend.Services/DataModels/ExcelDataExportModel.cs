using Raiffeisen.RegistarMjenicaBackend.Services.DataModels;

namespace Raiffeisen.RegistarMjenicaBackend.Services.DataModels;

public class ExcelDataExportModel
{
    public Dictionary<string, string> Headers { get; set; }
    public List<MjenicaGridModel> SelectedMjenice { get; set; }
}