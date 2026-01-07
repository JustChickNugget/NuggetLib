using System.Text.Json;
using NuggetLib.Views.Models.Api;

namespace NuggetLib.Views.Services;

/// <summary>
/// Update utilities.
/// </summary>
internal static class UpdateCheckService
{
    /// <summary>
    /// Check for updates using GitHub's API.
    /// </summary>
    /// <param name="repositoryLatestReleaseApiLink">API link to the latest release of the repository</param>
    /// <param name="applicationVersion">Current version of the application</param>
    /// <param name="cancellationToken">Cancellation token object</param>
    internal static async Task<bool> CheckForUpdatesAsync(
        string repositoryLatestReleaseApiLink,
        Version applicationVersion,
        CancellationToken cancellationToken = default)
    {
        using HttpClient client = new();
        client.DefaultRequestHeaders.Add("User-Agent", "NuggetLib");

        string response = await client.GetStringAsync(repositoryLatestReleaseApiLink, cancellationToken);
        GitHubLatestReleaseApiResponse? gitHubApiResponse =
            JsonSerializer.Deserialize<GitHubLatestReleaseApiResponse>(response);

        if (gitHubApiResponse == null)
        {
            throw new ArgumentNullException(null, "GitHub API response is null.");
        }

        if (gitHubApiResponse.TagName == null)
        {
            throw new ArgumentNullException(null, "Couldn't check version: tag name is null.");
        }

        string latestReleaseVersionString = gitHubApiResponse.TagName.StartsWith('v')
            ? gitHubApiResponse.TagName[1..]
            : gitHubApiResponse.TagName;

        latestReleaseVersionString = latestReleaseVersionString.Split('.').Length == 3
            ? latestReleaseVersionString + ".0"
            : latestReleaseVersionString;

        if (!Version.TryParse(latestReleaseVersionString, out Version? latestReleaseVersion))
        {
            throw new FormatException($"Invalid version format: {gitHubApiResponse.TagName}");
        }

        return latestReleaseVersion > applicationVersion;
    }
}