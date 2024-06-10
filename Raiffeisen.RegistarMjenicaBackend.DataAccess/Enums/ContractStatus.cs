using System.ComponentModel.DataAnnotations;

namespace DataAccess.Enums;

public enum ContractStatus
{
    [Display(Name = "Neaktivan")]
    Inactive,

    [Display(Name = "Aktivan")]
    Active,

    [Display(Name = "U procesu")]
    Drafted,
}