namespace DataAccess.Common.Interfaces;

public interface IAuditableEntity : IEntity
{
    /// <summary>
    ///     Unique identifier of the person that exempted Mjenica.
    ///     It's populated in the case of Mjenica exemption
    /// </summary>
    string? ExemptedBy { get; set; }


    /// <summary>
    ///     Date of the Mjenica exemption
    /// </summary>
    DateTime? ExemptionDate { get; set; }


    /// <summary>
    ///     Unique identifier of the person that verified Mjenica exemption.
    /// </summary>
    string? ExemptionVerifiedBy { get; set; }


    /// <summary>
    ///     Date of the Mjenica exemption verification
    /// </summary>
    public DateTime? ExemptionVerifiedDate { get; set; }
}