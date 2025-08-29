using Microsoft.AspNetCore.Authorization;

namespace AppTemplate.Core.Infrastructure.Authorization;

public sealed class HasPermissionAttribute : AuthorizeAttribute
{
  public HasPermissionAttribute(string permission)
      : base(permission)
  {
  }
}