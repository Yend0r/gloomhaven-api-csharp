using System;
namespace GloomChars.Authentication.Models
{
    public class AuthenticatedUser
    {
        public int Id { get; set; }                
        public string Email { get; set; }        
        public string Name { get; set; }
        public string AccessToken { get; set; }
        public DateTime AccessTokenExpiresAt { get; set; }
        public bool IsSystemAdmin { get; set; }
    }
}
