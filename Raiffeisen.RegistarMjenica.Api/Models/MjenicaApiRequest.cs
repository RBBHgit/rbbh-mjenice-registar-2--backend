using System.ComponentModel.DataAnnotations;
using Raiffeisen.RegistarMjenica.Api.Constants;
using Raiffeisen.RegistarMjenica.Api.Services.DataModels;

namespace Raiffeisen.RegistarMjenica.Api.Models;

public class MjenicaApiRequest
{
    [Required(ErrorMessage = ErrorMessages.MjenicaIsRequired)]
    public MjenicaModel Mjenica { get; set; }

    [Required(ErrorMessage = ErrorMessages.UsernameIsRequired)]
    public string LoggedInUser { get; set; }
}