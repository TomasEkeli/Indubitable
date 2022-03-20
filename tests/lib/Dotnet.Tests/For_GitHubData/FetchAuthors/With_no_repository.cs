using System.IO;

using Octokit;

namespace Dotnet.Tests.For_GitHubData.FetchAuthors;

[TestFixture]
public class With_no_repository
{
    IEnumerable<Author> _authors;

    [OneTimeSetUp]
    public async Task SetUp()
    {
        // read token from file
        var token = await File.ReadAllTextAsync("data/access.token");

        if (string.IsNullOrEmpty(token))
        {
            var tokenInteraction = new OAuthTokenInteraction();

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

            if (tokenResult?.success == false)
            {
                throw new Exception("failed to get token");
            }
            else
            {
                token = tokenResult.access_token;
            }
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