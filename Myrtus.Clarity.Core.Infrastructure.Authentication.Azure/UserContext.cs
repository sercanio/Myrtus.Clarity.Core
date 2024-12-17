﻿using Microsoft.AspNetCore.Http;
using Myrtus.Clarity.Core.Application.Abstractions.Authentication;

namespace Myrtus.Clarity.Core.Infrastructure.Authentication.Azure;

public sealed class UserContext : IUserContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserContext(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid UserId =>
        _httpContextAccessor
            .HttpContext?
            .User
            .GetUserId() ??
        throw new ApplicationException("User context is unavailable");

    public string IdentityId
    {
        get =>
            _httpContextAccessor
                .HttpContext?
                .User
                .GetIdentityId() ??
            throw new ApplicationException("User context is unavailable");
        set
        {
            // This setter can be used to store the identity ID in the HttpContext if needed.
            // For now, it does nothing as the identity ID is typically read from the claims.
        }
    }
}
