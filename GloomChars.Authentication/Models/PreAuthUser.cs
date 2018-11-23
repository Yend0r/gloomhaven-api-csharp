using System;

namespace GloomChars.Authentication.Models
{
    public class PreAuthUser
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public bool IsLockedOut { get; set; }
        public int LoginAttemptNumber { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public DateTime? DateLockedOut { get; set; }
    }
}

