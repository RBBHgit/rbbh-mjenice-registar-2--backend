using System.Runtime.CompilerServices;

namespace Raiffeisen.RegistarMjenicaBackend.Services.Exceptions;

public interface IRfLogger
{
    void LogError(Exception ex, string message, [CallerMemberName] string memberName = "",
        [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0);

    void LogWarn(string message);
    void LogInfo(string message);
    void LogDebug(string message);
}