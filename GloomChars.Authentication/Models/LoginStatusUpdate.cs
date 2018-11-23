using System;
namespace GloomChars.Authentication.Models
{
    public class LoginStatusUpdate
    {
        public int UserId { get; set; }
        public int AttemptNumber { get; set; }
        public bool IsLockedOut { get; set; }
        public DateTime? DateLockedOut { get; set; }
    }
}
