namespace AppTemplate.Core.Application.Abstractions.Authentication.Azure;

  public class AzureAdB2COptions
  {
      public string Instance { get; set; }
      public string ClientId { get; set; }
      public string TenantId { get; set; }
      public string SignUpSignInPolicyId { get; set; }
      public string RedirectUri { get; set; }
      public string ClientSecret { get; set; }
  }
