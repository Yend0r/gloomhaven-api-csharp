using System;
namespace GloomChars.Authentication.Models
{
    public class AuthenticationError
    {
        public AuthenticationError(string errorMsg)
        {
            Message = errorMsg;
        }

        public string Message { get; set; }
    }
}
