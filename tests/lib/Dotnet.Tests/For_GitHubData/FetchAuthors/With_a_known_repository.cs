using System.IO;
using System.Net.Http;

using Octokit;

namespace Dotnet.Tests.For_GitHubData.FetchAuthors;

[TestFixture]
[Category("Integration")]
public class With_a_known_repository
{
    IEnumerable<Author> _authors;

    [OneTimeSetUp]
    public async Task SetUp()
    {
        // read token from file
        var token = File.Exists("data/access.token") ?
            await File.ReadAllTextAsync("data/access.token")
            : "";

        if (string.IsNullOrEmpty(token))
        {
            var tokenInteraction = new OAuthTokenInteraction(new HttpClient());

            var verification = await tokenInteraction.RequestDeviceVerificationCode(
                clientId: "2c751fe4a634018d792d"
            );
            if (verification == null)
            {
                throw new Exception("failed to get verification code");
            }
            Console.WriteLine(
                $"verification needed. go to {verification.verification_uri} and enter {verification.user_code}"
            );

            var tokenResult = await tokenInteraction.RequestToken(verification);

            token = tokenResult?.success == false
                ? throw new Exception("failed to get token") : tokenResult.access_token;
            await File.WriteAllTextAsync("data/access.token", token);
        }

        var client = new GitHubClient(new ProductHeaderValue("Indubitable.Tests"))
        {
            Credentials = new Credentials(token)
        };


        var data = new GitHubData(client);

        _authors = await data.FetchAuthors(new Uri("https://github.com/nunit/nunit"));
    }

    [Test]
    [Category("Integration")]
    public void There_are_authors_of_the_well_known_repository() => _authors.Any().ShouldBeTrue();
}

[TestFixture]
public class With_a_url_that_does_not_go_to_github
{
    IEnumerable<Author> _authors;

    [OneTimeSetUp]
    public async Task SetUp()
    {
        var client = Substitute.For<IGitHubClient>();

        client
            .Miscellaneous
            .GetRateLimits()
            .Returns(
                new MiscellaneousRateLimit(new(), new RateLimit(1, 1, 1))
            );

        _authors = await new GitHubData(client).FetchAuthors(new Uri("https://not.a.repo"));
    }

    [Test]
    public void There_are_no_authors() => _authors.Any().ShouldBeFalse();
}