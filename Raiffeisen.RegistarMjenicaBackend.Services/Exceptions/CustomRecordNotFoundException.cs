namespace Raiffeisen.RegistarMjenica.Services.Exceptions;

public class CustomRecordNotFoundException : Exception
{
    public CustomRecordNotFoundException(string message, CustomProblemDetails? customProblemDetails) : base(message)
    {
        CustomProblemDetails = customProblemDetails;
    }

    public CustomProblemDetails CustomProblemDetails { get; set; } = new();
}