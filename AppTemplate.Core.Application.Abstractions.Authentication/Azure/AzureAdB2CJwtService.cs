using AppTemplate.Core.Domain.Abstractions;
using Ardalis.Result;
using Microsoft.Extensions.Options;
using AppTemplate.Core.Infrastructure.Authentication.Azure.Models;
using System.Net.Http.Json;

namespace AppTemplate.Core.Application.Abstractions.Authentication.Azure;

public sealed class AzureAdB2CJwtService : IJwtService
{
  private static readonly DomainError AuthenticationFailed = new(
      "AzureAdB2C.AuthenticationFailed",
      401,
      "Failed to acquire access token due to authentication failure");

  private readonly HttpClient _httpClient;
  private readonly AzureAdB2COptions _azureAdB2COptions;

  public AzureAdB2CJwtService(HttpClient httpClient, IOptions<AzureAdB2COptions> azureAdB2COptions)
  {
    _httpClient = httpClient;
    _azureAdB2COptions = azureAdB2COptions.Value;
  }

  public async Task<Result<string>> GetAccessTokenAsync(
      string email,
      string password,
      CancellationToken cancellationToken = default)
  {
    try
    {
      var authRequestParameters = new KeyValuePair<string, string>[]
      {
                  new("client_id", _azureAdB2COptions.ClientId),
                  new("client_secret", _azureAdB2COptions.ClientSecret),
                  new("scope", "openid profile email offline_access"),
                  new("grant_type", "password"),
                  new("username", email),
                  new("password", password)
      };

      using var authorizationRequestContent = new FormUrlEncodedContent(authRequestParameters);

      HttpResponseMessage response = await _httpClient.PostAsync(
          $"{_azureAdB2COptions.Instance}/oauth2/v2.0/token?p={_azureAdB2COptions.SignUpSignInPolicyId}",
          authorizationRequestContent,
          cancellationToken);

      response.EnsureSuccessStatusCode();

      AuthorizationToken? authorizationToken = await response
          .Content
          .ReadFromJsonAsync<AuthorizationToken>(cancellationToken);

      if (authorizationToken is null)
      {
        return Result.Forbidden(AuthenticationFailed.Code);
      }

      return authorizationToken.AccessToken;
    }
    catch (HttpRequestException)
    {
      return Result.Forbidden(AuthenticationFailed.Code);
    }
  }
}
