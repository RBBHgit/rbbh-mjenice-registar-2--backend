using System.Runtime.CompilerServices;
using NLog;


namespace Raiffeisen.RegistarMjenicaBackend.Services.Exceptions;

public class RfLogger : IRfLogger
{
    private readonly Logger _logger;

    public RfLogger()
    {
        _logger = LogManager.GetCurrentClassLogger();
    }

    public void LogError(Exception ex, string message, [CallerMemberName] string memberName = "",
        [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0)
    {
        Log(LogLevel.Error, message, ex, memberName, filePath, lineNumber);
    }

    public void LogWarn(string message)
    {
        Log(LogLevel.Warn, message);
    }

    public void LogInfo(string message)
    {
        Log(LogLevel.Info, message);
    }

    public void LogDebug(string message)
    {
        Log(LogLevel.Debug, message);
    }

    private void Log(LogLevel level, string message, Exception ex = null, string memberName = "", string filePath = "",
        int lineNumber = 0)
    {
        var trace = !string.IsNullOrEmpty(memberName)
            ? $"CallerMemberName: {memberName}, CallerFilePath: {filePath}, LineNumber: {lineNumber}."
            : "";

        var logEvent = new LogEventInfo(level, _logger.Name, message)
        {
            Exception = ex
        };

        if (!string.IsNullOrEmpty(trace))
            logEvent.Properties["Trace"] = trace;

        _logger.Log(logEvent);
    }
}