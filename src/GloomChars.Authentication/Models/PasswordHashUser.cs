using System;
namespace GloomChars.Authentication.Models
{
    public class PasswordHashUser
    {
        public PasswordHashUser(string email)
        {
            Email = email;
        }
        public string Email { get; set; }
    }
}
