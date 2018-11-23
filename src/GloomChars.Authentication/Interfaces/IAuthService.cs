using System;
using Bearded.Monads;
using GloomChars.Authentication.Models;

namespace GloomChars.Authentication.Interfaces
{
    public interface IAuthService
    {
        Either<AuthenticatedUser, string> Authenticate(string email, string password);
        Either<AuthenticatedUser, string> GetAuthenticatedUser(string accessToken);
        int RevokeToken(string accessToken);
    }
}
