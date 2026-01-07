using System.Diagnostics;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using NuggetLib.Core.Utilities;
using NuggetLib.Views.Services;

namespace NuggetLib.Views.Windows;

/// <summary>
/// Update check window.
/// </summary>
public partial class UpdateCheckWindow : Window
{
    /// <summary>
    /// Link to the latest release of the repository.
    /// </summary>
    private string RepositoryLatestReleaseLink { get; }

    /// <summary>
    /// API link to the latest release of the repository.
    /// </summary>
    private string RepositoryLatestReleaseApiLink { get; }

    /// <summary>
    /// Current version of the application.
    /// </summary>
    private Version ApplicationVersion { get; }

    /// <summary>
    /// A constructor of the update checker window.
    /// </summary>
    public UpdateCheckWindow(
        string repositoryLatestReleaseLink,
        string repositoryLatestReleaseApiLink,
        Version applicationVersion)
    {
        InitializeComponent();

        RepositoryLatestReleaseLink = repositoryLatestReleaseLink;
        RepositoryLatestReleaseApiLink = repositoryLatestReleaseApiLink;
        ApplicationVersion = applicationVersion;
    }

    #region Main events

    /// <summary>
    /// Check updates.
    /// </summary>
    /// <param name="sender">Sender object</param>
    /// <param name="e">Event arguments</param>
    private async void CheckButton_OnClick(object? sender, RoutedEventArgs e)
    {
        try
        {
            UpdateStatusLabel.Content = "Checking...";
            DownloadButton.IsEnabled = false;

            bool updatesAvailable = await UpdateCheckService.CheckForUpdatesAsync(
                RepositoryLatestReleaseApiLink,
                ApplicationVersion);

            if (updatesAvailable)
            {
                UpdateStatusLabel.Content = "New version available";
                DownloadButton.IsEnabled = true;
            }
            else
            {
                UpdateStatusLabel.Content = "Latest version";
                DownloadButton.IsEnabled = false;
            }
        }
        catch (Exception exception)
        {
            UpdateStatusLabel.Content = "An exception has occurred";
            DownloadButton.IsEnabled = false;

            DebugLogger.LogException(
                exception,
                nameof(UpdateCheckWindow),
                nameof(CheckButton_OnClick));

            await ExceptionHandleService.ShowExceptionAsync(
                this,
                exception,
                nameof(UpdateCheckWindow),
                nameof(CheckButton_OnClick));
        }
    }

    /// <summary>
    /// Open repository link.
    /// </summary>
    /// <param name="sender">Sender object</param>
    /// <param name="e">Event arguments</param>
    private async void DownloadButton_OnClick(object? sender, RoutedEventArgs e)
    {
        try
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = RepositoryLatestReleaseLink,
                UseShellExecute = true
            });
        }
        catch (Exception exception)
        {
            DebugLogger.LogException(
                exception,
                nameof(UpdateCheckWindow),
                nameof(DownloadButton_OnClick));

            await ExceptionHandleService.ShowExceptionAsync(
                this,
                exception,
                nameof(UpdateCheckWindow),
                nameof(DownloadButton_OnClick));
        }
    }

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
                nameof(UpdateCheckWindow),
                nameof(CloseButton_OnClick));

            await ExceptionHandleService.ShowExceptionAsync(
                this,
                exception,
                nameof(UpdateCheckWindow),
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
                nameof(UpdateCheckWindow),
                nameof(Window_OnKeyDown));

            await ExceptionHandleService.ShowExceptionAsync(
                this,
                exception,
                nameof(UpdateCheckWindow),
                nameof(Window_OnKeyDown));
        }
    }

    #endregion
}