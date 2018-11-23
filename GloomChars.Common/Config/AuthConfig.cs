using System;

namespace GloomChars.Common.Config
{
    public class AuthConfig
    {
        public int AccessTokenDurationInMins { get; set; }
        public bool UseLockout { get; set; }
        public int LoginAttemptsBeforeLockout { get; set; }
        public int LockoutDurationInMins { get; set; }
    }
}
