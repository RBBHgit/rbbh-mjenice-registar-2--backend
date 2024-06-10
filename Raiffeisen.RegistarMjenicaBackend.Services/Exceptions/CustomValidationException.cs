namespace Raiffeisen.RegistarMjenica.Services.Exceptions;

public class CustomValidationException : Exception
{
    public CustomValidationException(string message, string? logDetails) : base(message)
    {
        CustomDetails = logDetails;
    }

    public string? CustomDetails { get; set; }
}