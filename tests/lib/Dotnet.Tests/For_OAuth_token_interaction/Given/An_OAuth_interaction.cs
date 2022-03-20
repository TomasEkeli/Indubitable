using System.Net.Http;
using RichardSzalay.MockHttp;

namespace Dotnet.Tests.For_OAuth_token_interaction.Given;

public class An_OAuth_interaction
{
    protected OAuthTokenInteraction _interaction;
    protected MockHttpMessageHandler _handler;

    public An_OAuth_interaction()
    {
        _handler = new MockHttpMessageHandler();
        var client = new HttpClient(_handler);
        _interaction = new OAuthTokenInteraction(client);
    }
}
