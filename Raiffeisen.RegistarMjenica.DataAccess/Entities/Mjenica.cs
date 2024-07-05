using System.ComponentModel.DataAnnotations.Schema;
using DataAccess.Common;
using DataAccess.Enums;

namespace DataAccess.Entities;

public class Mjenica : BaseAuditableEntity
{
    [Column("JMBG", Order = 1)] public string JMBG { get; set; }

    [Column("ClientName", Order = 2)] public string? ClientName { get; set; }

    [Column("ClientMjenicaSerialNumber", Order = 3)]
    public string? ClientMjenicaSerialNumber { get; set; }

    [Column("GuarantorMjenicaSerialNumber", Order = 4)]
    public string? GuarantorMjenicaSerialNumber { get; set; }

    [Column("ContractNumber", Order = 5)] public string? ContractNumber { get; set; }

    [Column("ContractDate", Order = 6)] public DateTime? ContractDate { get; set; }

    [Column("ContractStatus", Order = 7)] public string? ContractStatus { get; set; }


    /// <summary>
    ///     Partija
    /// </summary>
    [Column("GroupNumber", Order = 8)]
    public string? GroupNumber { get; set; }
    

    [Column("CreatedBy", Order = 9)]
    public string? CreatedBy { get; set; }
    
    [Column("CreatedDate", Order = 10)]
    public DateTime? CreatedDate { get; set; }
    
    [Column("IsCreatedVerified", Order = 11)]
    public bool? IsCreatedVerified { get; set; }
    
    [Column("CreationVerifiedBy", Order = 12)]
    public string? CreationVerifiedBy { get; set; }
    

    [Column("CreationVerifiedDate", Order = 13)]
    public DateTime? CreationVerifiedDate { get; set; }

    [Column("IsExemptedVerified", Order = 14)]
    public bool? IsExemptedVerified { get; set; }

    [Column("IsExempted", Order = 15)] public bool? IsExempted { get; set; }

    [Column("ExemptionReason", Order = 16)]
    public ExemptionReason? ExemptionReason { get; set; }

    [Column("ObtainedOriginalDocuments", Order = 17)]
    public string? ObtainedOriginalDocuments { get; set; }

    [Column("TransferedToContractNumber", Order = 18)]
    public string? TransferedToContractNumber { get; set; }

    [Column("FreeTextField", Order = 19)] public string? FreeTextField { get; set; }

    public virtual IEnumerable<MjenicaExemptionHistory> MjenicaExemptionHistory { get; set; }
}