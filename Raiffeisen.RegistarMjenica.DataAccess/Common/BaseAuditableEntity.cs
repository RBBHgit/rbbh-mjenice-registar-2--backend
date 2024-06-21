using DataAccess.Common.Interfaces;

namespace DataAccess.Common;

public class BaseAuditableEntity : BaseEntity, IAuditableEntity
{
    public string? ExemptedBy { get; set; }

    public DateTime? ExemptionDate { get; set; }

    public string? ExemptionVerifiedBy { get; set; }

    public DateTime? ExemptionVerifiedDate { get; set; }
}