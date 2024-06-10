namespace Raiffeisen.RegistarMjenicaBackend.Services.DataModels.Base;

public class BaseAuditableModel : BaseModel
{
    public string? ExemptedBy { get; set; }

    public DateTime? ExemptionDate { get; set; }

    public string? ExemptionVerifiedBy { get; set; }

    public DateTime? ExemptionVerifiedDate { get; set; }
}