using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using NuggetLib.Core.Utilities;

namespace NuggetLib.Views.Windows;

/// <summary>
/// Window that is displayed when an exception occurs.
/// </summary>
internal partial class ExceptionWindow : Window
{
    /// <summary>
    /// A constructor of the exception window.
    /// </summary>
    /// <param name="occurredException">Occurred exception data</param>
    /// <param name="className">Name of the class where the exception occurred</param>
    /// <param name="functionName">Name of the function where the exception occurred</param>
    internal ExceptionWindow(Exception occurredException, string className, string functionName)
    {
        InitializeComponent();

        ExceptionTypeTextBox.Text = occurredException.GetType().FullName;
        ExceptionMessageTextBox.Text = occurredException.Message;
        ClassNameTextBox.Text = className;
        FunctionNameTextBox.Text = functionName;
        StackTraceTextBox.Text = occurredException.ToString();
    }

    #region Main events

    /// <summary>
    /// Close current window.
    /// </summary>
    /// <param name="sender">Sender object</param>
    /// <param name="e">Event arguments</param>
    private void CloseButton_OnClick(object? sender, RoutedEventArgs e)
    {
        try
        {
            Close();
        }
        catch (Exception exception)
        {
            DebugLogger.LogException(
                exception,
                nameof(ExceptionWindow),
                nameof(CloseButton_OnClick));
        }
    }

    #endregion

    #region Window events

    /// <summary>
    /// Handle user input.
    /// </summary>
    /// <param name="sender">Sender object</param>
    /// <param name="e">Event arguments</param>
    private void Window_OnKeyDown(object? sender, KeyEventArgs e)
    {
        try
        {
            if (e.Key != Key.Enter)
            {
                return;
            }

            e.Handled = true;
            CloseButton_OnClick(sender, e);
        }
        catch (Exception exception)
        {
            DebugLogger.LogException(
                exception,
                nameof(ExceptionWindow),
                nameof(Window_OnKeyDown));
        }
    }

    #endregion
}