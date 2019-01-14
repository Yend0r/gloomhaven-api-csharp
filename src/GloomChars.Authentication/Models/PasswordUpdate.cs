using System;
namespace GloomChars.Authentication.Models
{
    public class PasswordUpdate
    {
        public string AccessToken { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
