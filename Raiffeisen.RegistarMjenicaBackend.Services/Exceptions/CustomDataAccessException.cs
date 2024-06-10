using Microsoft.EntityFrameworkCore;

namespace Raiffeisen.RegistarMjenica.Services.Exceptions;

public class CustomDataAccessException : Exception
{
    public CustomDataAccessException(string message, int logId, string serviceType = null) : base(message)
    {
        LogId = logId;
        ServiceType = serviceType;
    }

    public CustomDataAccessException(string message, string serviceType = null) : base(message)
    {
        ServiceType = serviceType;
    }

    public int LogId { get; set; }
    public string? ServiceType { get; set; }
    public DbUpdateException DbUpdateException { get; set; }
    public OperationCanceledException OperationCanceledException { get; set; }
}