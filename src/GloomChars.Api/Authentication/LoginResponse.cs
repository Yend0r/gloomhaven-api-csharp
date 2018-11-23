using System;
using GloomChars.Authentication.Models;

namespace GloomChars.Api.Authentication
{
    public class LoginResponse
    {
        public string Email { get; set; }
        public string AccessToken { get; set; }
        public DateTime AccessTokenExpiresAt { get; set; }

        public LoginResponse() { }

        public LoginResponse(AuthenticatedUser user)
        {
            Email = user.Email;
            AccessToken = user.AccessToken;
            AccessTokenExpiresAt = user.AccessTokenExpiresAt;
        }
    }
}
