using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DataAccess.Common;
using DataAccess.Enums;

namespace DataAccess.Entities;

public class MjenicaExemptionHistory : BaseAuditableEntity
{
    [ForeignKey("MjenicaId")]
    [Column("MjenicaId", Order = 1)]
    public int MjenicaId { get; set; }

    public virtual Mjenica Mjenica { get; set; }

    public string? ObtainedOriginalDocuments { get; set; }

    [Required] public ExemptionReason ExemptionReason { get; set; }

    public bool IsExemptionVerified { get; set; }

    public string? ContractNumber { get; set; }

    public string? TransferedToContractNumber { get; set; }
}