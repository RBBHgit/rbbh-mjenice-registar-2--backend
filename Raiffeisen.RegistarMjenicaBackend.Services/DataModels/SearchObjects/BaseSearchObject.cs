namespace Raiffeisen.RegistarMjenicaBackend.Services.DataModels.SearchObjects;

public class BaseSearchObject
{
    public int? Page { get; set; }
    public int? Skip { get; set; }
    public int? Take { get; set; }
    public int? PageSize { get; set; }
    public bool? IsDistinct { get; set; }
    public int TotalRecord { get; set; } = 0;
}