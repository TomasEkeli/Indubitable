using RichardSzalay.MockHttp;

namespace Dotnet.Tests.For_OAuth_token_interaction.RequestDeviceVerificationCode;

public class With_a_client_id : Given.An_OAuth_interaction
{
    DeviceVerification _verification;

    [SetUp]
    public async Task Request_verification_with_a_client_id()
    {
        _handler
            .Expect("/login/device/code")
            .WithHeaders(
                new KeyValuePair<string, string>[]
                {
                    new ("User-Agent", "Indubitable"),
                    new ("Accept", "application/json")
                }
            )
            .Respond("application/json", @"{}");

        _verification = await _interaction.RequestDeviceVerificationCode(
            clientId: "2c751fe4a634018d792d"
        );
    }

    [Test]
    public void A_verification_was_made() => _verification.ShouldNotBeNull();

    [Test]
    public void A_call_to_github_was_made() =>
        _handler
            .VerifyNoOutstandingRequest();
}