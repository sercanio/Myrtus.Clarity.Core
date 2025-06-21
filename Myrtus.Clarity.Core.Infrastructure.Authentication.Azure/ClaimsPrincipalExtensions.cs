using System.Security.Claims;
using Microsoft.IdentityModel.JsonWebTokens;

namespace Myrtus.Clarity.Core.Infrastructure.Authentication.Azure;

public static class ClaimsPrincipalExtensions
{
    public static Guid GetUserId(this ClaimsPrincipal? principal)
    {
        string? userId = principal?.FindFirstValue(JwtRegisteredClaimNames.Sub);

        return Guid.TryParse(userId, out Guid parsedUserId) ?
            parsedUserId :
            throw new ApplicationException("User id is unavailable");
    }

    public static string GetIdentityId(this ClaimsPrincipal? principal)
    {
        string? identityId = principal?.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(identityId) &&
            principal?.Identity?.AuthenticationType == "Identity.TwoFactorUserId")
        {
            identityId = principal.Identity.Name;
        }

        return identityId ?? throw new ApplicationException("User identity is unavailable");
    }
}
