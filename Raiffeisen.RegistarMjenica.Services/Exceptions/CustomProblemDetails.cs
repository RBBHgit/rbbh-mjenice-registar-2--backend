using Microsoft.AspNetCore.Mvc;

namespace Raiffeisen.RegistarMjenica.Services.Exceptions;

public class CustomProblemDetails : ProblemDetails
{
    public int? LogId { get; set; }
}