using System;
namespace GloomChars.Api.Authentication
{
    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
