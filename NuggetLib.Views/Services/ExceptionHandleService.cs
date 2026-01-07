using Avalonia.Controls;
using NuggetLib.Views.Windows;

namespace NuggetLib.Views.Services;

/// <summary>
/// Utilities for handling exceptions using views.
/// </summary>
public static class ExceptionHandleService
{
    /// <summary>
    /// Open a window with the exception information.
    /// </summary>
    /// <param name="ownerWindow">Window in which the exception occurred</param>
    /// <param name="exception">Exception object</param>
    /// <param name="className">Name of the class where the exception occurred</param>
    /// <param name="functionName">Name of the function where the exception occurred</param>
    public static async Task ShowExceptionAsync(
        Window ownerWindow,
        Exception exception,
        string className,
        string functionName)
    {
        ExceptionWindow exceptionWindow = new(exception, className, functionName);
        await exceptionWindow.ShowDialog(ownerWindow);
    }
}