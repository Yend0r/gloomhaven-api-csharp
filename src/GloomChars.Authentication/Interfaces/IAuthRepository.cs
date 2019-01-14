using System;
using Bearded.Monads;
using GloomChars.Authentication.Models;

namespace GloomChars.Authentication.Interfaces
{
    public interface IAuthRepository
    {
        Option<AuthenticatedUser> GetAuthenticatedUser(string accessToken);
        Option<PreAuthUser> GetUserForAuth(string email);
        Option<PreAuthUser> GetUserByToken(string accessToken);
        Either<NewLogin, string> InsertNewLogin(NewLogin newLogin);
        void UpdateLoginStatus(LoginStatusUpdate statusUpdate);
        int RevokeToken(string accessToken);
        int UpdatePassword(int userId, string passwordHash);
    }
}
