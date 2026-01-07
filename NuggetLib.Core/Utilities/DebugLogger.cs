#if DEBUG
using System.Diagnostics;
#endif

namespace NuggetLib.Core.Utilities;

/// <summary>
/// Debug log utilities.
/// </summary>
public static class DebugLogger
{
    /// <summary>
    /// Log message in the debug console.
    /// </summary>
    /// <param name="message">Debug message</param>
    /// <param name="className">Name of the class in which the message is printed</param>
    /// <param name="functionName">Name of the function in which the message is printed</param>
    public static void LogMessage(string message, string className, string functionName)
    {
#if DEBUG
        Debug.WriteLine($"[{className} -> {functionName}]: \"{message}\"");
#endif
    }

    /// <summary>
    /// Log warning message in the debug console.
    /// </summary>
    /// <param name="warningMessage">Debug warning message</param>
    /// <param name="className">Name of the class in which the message is printed</param>
    /// <param name="functionName">Name of the function in which the message is printed</param>
    public static void LogWarning(string warningMessage, string className, string functionName)
    {
#if DEBUG
        Debug.WriteLine($"[{className} -> {functionName}] Warning: \"{warningMessage}\"");
#endif
    }

    /// <summary>
    /// Log exception in the debug console.
    /// </summary>
    /// <param name="exception">Exception object</param>
    /// <param name="className">Name of the class in which the exception has occurred</param>
    /// <param name="functionName">Name of the function in which the exception has occurred</param>
    public static void LogException(Exception exception, string className, string functionName)
    {
#if DEBUG
        Debug.WriteLine(
            $"[{className} -> {functionName}] An exception has occurred ({exception.GetType().FullName}): \"{exception.Message}\"");
#endif
    }
}