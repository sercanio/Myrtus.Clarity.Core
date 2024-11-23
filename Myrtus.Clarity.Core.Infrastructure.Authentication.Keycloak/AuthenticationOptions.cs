﻿namespace Myrtus.Clarity.Core.Infrastructure.Authentication.Keycloak;
public sealed class AuthenticationOptions
{
    public string Audience { get; set; } = string.Empty;

    public string MetadataUrl { get; set; } = string.Empty;

    public bool RequireHttpsMetadata { get; init; }

    public string Issuer { get; set; } = string.Empty;
}
