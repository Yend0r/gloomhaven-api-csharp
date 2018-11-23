using System;
using Microsoft.AspNetCore.Authentication;

namespace GloomChars.Api.Authentication
{
    public class BearerTokenDefaults
    {
        public const string AuthenticationScheme = "Bearer";

        public const string AccessTokenClaimName = "AccessToken";

        public static void AuthenticationOptions(AuthenticationOptions options)
        {
            options.DefaultAuthenticateScheme = AuthenticationScheme;
            options.DefaultChallengeScheme = AuthenticationScheme;
        }
    }
}
