using System;
using System.Security.Claims;
using Bearded.Monads;
using GloomChars.Api.Errors;
using Microsoft.AspNetCore.Mvc;

namespace GloomChars.Api.Authentication
{
    public class UserManager : IUserManager
    {
        public Either<TokenUser, IApiError> GetCurrentUser(ClaimsPrincipal principal)
        {
            if (principal is TokenUser currentUser)
            {
                return currentUser;
            }

            return new UnauthorizedError(); //This really should not happen
        }
    }
}
