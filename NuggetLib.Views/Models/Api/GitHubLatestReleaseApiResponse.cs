using System.Text.Json.Serialization;

namespace NuggetLib.Views.Models.Api;

/// <summary>
/// Contains GitHub's API response attributes.
/// </summary>
public sealed record GitHubLatestReleaseApiResponse
{
    /// <summary>
    /// Tag name of the latest release from the response.
    /// </summary>
    [JsonPropertyName("tag_name")]
    public required string TagName { get; init; }
}