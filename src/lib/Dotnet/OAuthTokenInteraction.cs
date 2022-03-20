using System.Net.Http.Json;

namespace Dotnet;

public class OAuthTokenInteraction
{
    readonly HttpClient _client;

    public OAuthTokenInteraction(HttpClient client)
    {
        _client = client;

        _client.BaseAddress = new Uri("https://github.com/");
        _client.DefaultRequestHeaders.Add("User-Agent", "Indubitable");
        _client.DefaultRequestHeaders.Add("Accept", "application/json");
    }

    public async Task<DeviceVerification?> RequestDeviceVerificationCode(
        string clientId,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(clientId))
        {
            return null;
        }
        var deviceResponse = await _client.PostAsJsonAsync(
            "https://github.com/login/device/code",
            new
            {
                client_id = clientId
            },
            cancellationToken
        );

        var deviceVerification = await deviceResponse
            .Content
            .ReadFromJsonAsync<DeviceVerification>(
                cancellationToken: cancellationToken
            );

        return deviceVerification == null
            ? null
            : deviceVerification with
            {
                expires = DateTimeOffset.Now.AddSeconds(deviceVerification.expires_in),
                client_id = clientId
            };
    }

    public async Task<TokenResult?> RequestToken(
        DeviceVerification verification,
        CancellationToken cancellationToken = default)
    {
        var alive = true;
        var interval = verification.interval;

        while (alive
            && DateTimeOffset.Now < verification.expires
            && cancellationToken.IsCancellationRequested == false
        )
        {
            var tokenResponse = await _client
                .PostAsJsonAsync(
                    "/login/oauth/access_token",
                    new
                    {
                        verification.client_id,
                        verification.device_code,
                        grant_type = "urn:ietf:params:oauth:grant-type:device_code"
                    },
                    cancellationToken: cancellationToken
                );

            if (tokenResponse is null)
            {
                return new();
            }

            var tokenResult =  await tokenResponse
                .Content
                .ReadFromJsonAsync<TokenResult>(
                    cancellationToken: cancellationToken
                );

            if (tokenResult is null)
            {
                return null;
            }

            if (string.IsNullOrEmpty(tokenResult.access_token) == false)
            {
                return tokenResult with
                {
                    success = true
                };
            }

            switch (tokenResult.error)
            {
                case "authorization_pending":
                    await Task.Delay(interval + 1, cancellationToken);
                    break;
                case "slow_down":
                    interval *= 2;
                    await Task.Delay(interval, cancellationToken);
                    break;
                case "":
                    alive = false;
                    break;
                default:
                    return new();
            }
        }
        return new();
    }
}

public record DeviceVerification(
    string device_code,
    string user_code,
    string verification_uri,
    int expires_in,
    int interval,
    string client_id,
    DateTimeOffset expires
);

public record TokenResult(
    bool success = false,
    string? access_token = null,
    string? token_type = null,
    string? scope = null,
    string? error = null,
    string? error_description = null
);