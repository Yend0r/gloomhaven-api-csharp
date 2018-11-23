using System;
using Microsoft.AspNetCore.Authentication;
using GloomChars.Api.Authentication;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class BearerTokenExtensions
    {
        public static AuthenticationBuilder AddBearerToken(this AuthenticationBuilder builder)
            => builder.AddScheme<BearerTokenAuthOptions, BearerTokenAuthHandler>(BearerTokenDefaults.AuthenticationScheme, _ => { });
    }
}
