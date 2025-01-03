﻿using System.Text.Json.Serialization;

namespace Myrtus.Clarity.Core.Infrastructure.Authentication.Azure.Models;

public sealed class AuthorizationToken
{
    [JsonPropertyName("access_token")]
    public string AccessToken { get; init; } = string.Empty;
}
