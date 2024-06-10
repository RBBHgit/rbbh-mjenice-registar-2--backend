namespace Raiffeisen.RegistarMjenicaBackend.Services.DataModels.SearchObjects;

public class MjenicaSearchObject : BaseSearchObject
{
    public string ClientMjenicaSerialNumber { get; set; }
    public string GuarantorMjenicaSerialNumber { get; set; }
    public string ClientName { get; set; }
    public string JMBG { get; set; }
    public string ContractNumber { get; set; }
    public string ContractStatus { get; set; }
    public string GroupNumber { get; set; }
    public DateTime? ContractDateStart { get; set; }
    public DateTime? ContractDateEnd { get; set; }
    public DateTime? CreatedDateStart { get; set; }
    public DateTime? CreatedDateEnd { get; set; }
    public bool? IsCreatedVerified { get; set; }
    public bool IncludeExemptionHistory { get; set; } = false;
    public int? Id { get; set; }
    public SortProperty SortDescriptor { get; set; }
}

public class SortProperty
{
    public bool Ascending { get; set; } = true;
    public string Property { get; set; }
}