namespace Raiffeisen.RegistarMjenica.Api.Services.DataModels;

public class MjeniceGridModel
{
    public string Id { get; set; }
    public string ClientMjenicaSerialNumber { get; set; }
    public string GuarantorMjenicaSerialNumber { get; set; }
    public string ClientName { get; set; }
    public DateTime? ContractDate { get; set; }
    public string ContractStatus { get; set; }
    public string JMBG { get; set; }
    public string GroupNumber { get; set; }
    public string ContractNumber { get; set; }
    public DateTime? CreatedDate { get; set; }
    public bool IsCreatedVerified { get; set; }
}