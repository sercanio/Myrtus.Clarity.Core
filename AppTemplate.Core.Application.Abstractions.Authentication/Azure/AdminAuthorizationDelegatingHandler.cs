using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using AppTemplate.Core.Infrastructure.Authentication.Azure.Models;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace AppTemplate.Core.Application.Abstractions.Authentication.Azure;

public sealed class AdminAuthorizationDelegatingHandler : DelegatingHandler
{
    private readonly AzureAdB2COptions _azureAdB2COptions;

    public AdminAuthorizationDelegatingHandler(IOptions<AzureAdB2COptions> azureAdB2COptions)
    {
        _azureAdB2COptions = azureAdB2COptions.Value;
    }

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        AuthorizationToken authorizationToken = await GetAuthorizationToken(cancellationToken);

        request.Headers.Authorization = new AuthenticationHeaderValue(
            JwtBearerDefaults.AuthenticationScheme,
            authorizationToken.AccessToken);

        HttpResponseMessage httpResponseMessage = await base.SendAsync(request, cancellationToken);

        httpResponseMessage.EnsureSuccessStatusCode();

        return httpResponseMessage;
    }

    private async Task<AuthorizationToken> GetAuthorizationToken(CancellationToken cancellationToken)
    {
        var authorizationRequestParameters = new KeyValuePair<string, string>[]
        {
            new("client_id", _azureAdB2COptions.ClientId),
            new("client_secret", _azureAdB2COptions.ClientSecret),
            new("scope", "openid email"),
            new("grant_type", "client_credentials")
        };

        var authorizationRequestContent = new FormUrlEncodedContent(authorizationRequestParameters);

        using var authorizationRequest = new HttpRequestMessage(
            HttpMethod.Post,
            new Uri(_azureAdB2COptions.TenantId))
        {
            Content = authorizationRequestContent
        };

        HttpResponseMessage authorizationResponse = await base.SendAsync(authorizationRequest, cancellationToken);

        authorizationResponse.EnsureSuccessStatusCode();

        return await authorizationResponse.Content.ReadFromJsonAsync<AuthorizationToken>(cancellationToken) ??
               throw new ApplicationException();
    }
}
