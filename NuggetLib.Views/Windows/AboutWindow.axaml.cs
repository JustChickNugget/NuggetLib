using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using NuggetLib.Core.Utilities;
using NuggetLib.Views.Services;

namespace NuggetLib.Views.Windows;

/// <summary>
/// About application window.
/// </summary>
public partial class AboutWindow : Window
{
    /// <summary>
    /// A constructor of the about window.
    /// </summary>
    /// <param name="applicationName">Name of the application</param>
    /// <param name="applicationDescription">Description of the application</param>
    /// <param name="applicationDeveloperLink">Link to the developer page</param>
    /// <param name="applicationRepositoryLink">Link to the repository page</param>
    /// <param name="applicationVersion">Version of the application</param>
    public AboutWindow(
        string applicationName,
        string applicationDescription,
        string applicationDeveloperLink,
        string applicationRepositoryLink,
        Version applicationVersion)
    {
        InitializeComponent();

        ApplicationNameTextBlock.Text = applicationName;
        ApplicationDescriptionTextBlock.Text = applicationDescription;
        ApplicationDeveloperHyperlinkButton.NavigateUri = new Uri(applicationDeveloperLink);
        ApplicationRepositoryHyperlinkButton.NavigateUri = new Uri(applicationRepositoryLink);

        ApplicationVersionLabel.Text =
            $"v{applicationVersion.Major}.{applicationVersion.Minor}.{applicationVersion.Build}";
    }

    #region Main events

    /// <summary>
    /// Close current window.
    /// </summary>
    /// <param name="sender">Sender object</param>
    /// <param name="e">Event arguments</param>
    private async void CloseButton_OnClick(object? sender, RoutedEventArgs e)
    {
        try
        {
            Close();
        }
        catch (Exception exception)
        {
            DebugLogger.LogException(
                exception,
                nameof(AboutWindow),
                nameof(CloseButton_OnClick));

            await ExceptionHandleService.ShowExceptionAsync(
                this,
                exception,
                nameof(AboutWindow),
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
    private async void Window_OnKeyDown(object? sender, KeyEventArgs e)
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
                nameof(AboutWindow),
                nameof(Window_OnKeyDown));

            await ExceptionHandleService.ShowExceptionAsync(
                this,
                exception,
                nameof(AboutWindow),
                nameof(Window_OnKeyDown));
        }
    }

    #endregion
}