using Octokit;

namespace Dotnet;

public interface IGitHubData
{
    Task<IEnumerable<Author>> FetchAuthors(
        Uri repositoryUrl,
        CancellationToken cancellationToken = default);
}

public class GitHubData : IGitHubData
{
    readonly IGitHubClient _client;

    public GitHubData(IGitHubClient client)
    {
        _client = client;
    }

    public async Task<IEnumerable<Author>> FetchAuthors(
        Uri repositoryUrl,
        CancellationToken cancellationToken = default
    )
    {
        var rateLimit = await _client.Miscellaneous.GetRateLimits();
        if (rateLimit.Rate.Remaining == 0)
        {
            throw new Exception("rate limited");
        }

        var owner = repositoryUrl.Segments[1].Trim('/');
        var name = repositoryUrl.Segments[2].Trim('/');

        var contributors = await _client.Repository.GetAllContributors(owner, name);

        return contributors?.Select(c => new Author(c.Login)) ?? Array.Empty<Author>();
    }
}
