using System;
using System.Security.Claims;
using GloomChars.Authentication.Models;

namespace GloomChars.Api.Authentication
{
    public class TokenUser : ClaimsPrincipal
    {
        public TokenUser(AuthenticatedUser user, ClaimsIdentity identity)
            : base(identity)
        {
            Id = user.Id;
            Email = user.Email;
            AccessToken = user.AccessToken;
            IsSystemAdmin = user.IsSystemAdmin;
        }

        public int Id { get; set; }
        public string Email { get; set; }
        public string AccessToken { get; set; }
        public bool IsSystemAdmin { get; set; }
    }
}
