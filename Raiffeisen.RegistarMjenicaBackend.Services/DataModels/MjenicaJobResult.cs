namespace Raiffeisen.RegistarMjenicaBackend.Services.DataModels;

public class MjenicaJobResult
{
    public MjenicaJobResult()
    {
    }

    public MjenicaJobResult(int logId, int statusCode, string status, string message)
    {
        LogId = logId;
        StatusCode = statusCode;
        Status = status;
        Message = message;
    }

    public int LogId { get; set; }
    public int StatusCode { get; set; }
    public string Status { get; set; }
    public string Message { get; set; }
}