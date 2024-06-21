using System.ComponentModel.DataAnnotations;
using DataAccess.Enums;
using Raiffeisen.RegistarMjenica.Api.Services.DataModels.Base;

namespace Raiffeisen.RegistarMjenica.Api.Services.DataModels;

public class MjenicaModel : BaseAuditableModel
{
    [Required] public string ClientMjenicaSerialNumber { get; set; }

    [Required] public string GuarantorMjenicaSerialNumber { get; set; }

    public string JMBG { get; set; } = string.Empty;

    public string ClientName { get; set; } = string.Empty;

    public string ContractNumber { get; set; } = string.Empty;

    public string? TransferedToContractNumber { get; set; }

    /// <summary>
    ///     Partija
    /// </summary>
    public string GroupNumber { get; set; } = string.Empty;

    public DateTime? ContractDate { get; set; } = DateTime.Now;

    public string ContractStatus { get; set; } = string.Empty;

    public bool? IsMjenicaExempted { get; set; }

    public bool? IsExemptedVerified { get; set; }

    public bool IsExempted { get; set; } = false;

    public bool IsCreatedVerified { get; set; } = false;

    public string? ObtainedOriginalDocuments { get; set; }

    public string? FreeTextField { get; set; }

    public DateTime? ExemptionDate { get; set; }

    public ExemptionReason? ExemptionReason { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; } = DateTime.Now;

    public string? CreationVerifiedBy { get; set; }

    public DateTime? CreationVerifiedDate { get; set; }

    public string? ExemptedBy { get; set; }

    public string? ExemptionVerifiedBy { get; set; }

    public DateTime? ExemptionVerifiedDate { get; set; }

    public bool? HasHistory { get; set; }
}