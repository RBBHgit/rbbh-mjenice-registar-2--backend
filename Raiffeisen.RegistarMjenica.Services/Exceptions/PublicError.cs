namespace Raiffeisen.RegistarMjenica.Services.Exceptions;

public class PublicError
{
    public int LogId { get; set; }
    public int StatusCode { get; set; }
    public string Message { get; set; }
    public string ErrorDetails { get; set; }
}