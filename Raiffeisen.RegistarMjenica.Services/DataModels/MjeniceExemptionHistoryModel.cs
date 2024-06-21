using Raiffeisen.RegistarMjenica.Api.Services.DataModels.Base;

namespace Raiffeisen.RegistarMjenica.Api.Services.DataModels;

public class MjeniceExemptionHistoryModel : BaseAuditableModel
{
    public string ObtainedOriginalDocuments { get; set; }
    public string ContractNumber { get; set; }
    public string TransferedToContractNumber { get; set; }
}