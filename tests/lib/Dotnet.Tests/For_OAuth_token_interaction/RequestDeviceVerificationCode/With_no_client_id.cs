namespace Dotnet.Tests.For_OAuth_token_interaction.RequestDeviceVerificationCode;

public class With_no_client_id : Given.An_OAuth_interaction
{
    DeviceVerification _verification;

    [SetUp]
    public async Task Request_verification_with_no_client_id()
    {
        _verification = await _interaction.RequestDeviceVerificationCode(
            clientId: ""
        );
    }

    [Test]
    public void No_verification_was_made() => _verification.ShouldBeNull();

    [Test]
    public void No_call_to_github_was_made() =>
        _handler
            .VerifyNoOutstandingRequest();
}
