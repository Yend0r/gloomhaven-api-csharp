using System;
using System.Security.Claims;
using Bearded.Monads;
using GloomChars.Api.Errors;
using Microsoft.AspNetCore.Mvc;

namespace GloomChars.Api.Authentication
{
    public interface IUserManager
    {
        Either<TokenUser, IApiError> GetCurrentUser(ClaimsPrincipal principal);
    }
}
