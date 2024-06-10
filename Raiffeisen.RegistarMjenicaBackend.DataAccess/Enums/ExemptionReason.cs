using System.ComponentModel.DataAnnotations;

namespace DataAccess.Enums;

public enum ExemptionReason
{
    [Display(Name ="Blokada")]
    Blocked,

    [Display(Name = "Vraćena klijentu")]
    ReturnedToClient,

    [Display(Name = "Prenesena na drugi ugovor")]
    TransferredToAnotherContract,

    [Display(Name = "Vraćeno na dopunu podataka")]
    ReturnedToDataImport
}