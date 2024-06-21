using DataAccess.Enums;
using Raiffeisen.RegistarMjenica.Api.Services.DataModels.Base;

namespace Raiffeisen.RegistarMjenica.Api.Services.DataModels;

public class MjenicaHistoryGridModel : BaseAuditableModel
{
    public string ObtainedOriginalDocuments { get; set; }

    public ExemptionReason ExemptionReason { get; set; }

    public string? ContractNumber { get; set; }

    public string? TransferedToContractNumber { get; set; }

    public string BadgeColor { get; set; }
}