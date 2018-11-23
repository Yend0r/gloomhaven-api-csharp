using System;
using Microsoft.AspNetCore.Authentication;

namespace GloomChars.Api.Authentication
{
    public class BearerTokenAuthOptions : AuthenticationSchemeOptions
    {
        public BearerTokenAuthOptions()
        {
        }
    }
}
