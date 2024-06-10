using DataAccess.Enums;
using Raiffeisen.RegistarMjenicaBackend.Services.DataModels.Base;

namespace Raiffeisen.RegistarMjenicaBackend.Services.DataModels;

public class MjenicaHistoryGridModel : BaseAuditableModel
{
    public string ObtainedOriginalDocuments { get; set; }

    public ExemptionReason ExemptionReason { get; set; }

    public string? ContractNumber { get; set; }

    public string? TransferedToContractNumber { get; set; }

    public string BadgeColor { get; set; }
}