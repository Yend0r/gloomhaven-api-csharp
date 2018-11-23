using System;
namespace GloomChars.Authentication.Models
{
    public class NewLogin
    {
        public int UserId { get; set; }
        public string AccessToken { get; set; }
        public DateTime DateExpires { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
