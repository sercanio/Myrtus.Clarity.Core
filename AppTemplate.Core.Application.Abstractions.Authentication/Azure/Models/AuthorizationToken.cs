using System.Text.Json.Serialization;

namespace AppTemplate.Core.Infrastructure.Authentication.Azure.Models;

public sealed class AuthorizationToken
{
    [JsonPropertyName("access_token")]
    public string AccessToken { get; init; } = string.Empty;
}
