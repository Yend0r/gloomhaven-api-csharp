using System;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using GloomChars.Authentication.Models;
using GloomChars.Authentication.Interfaces;
using Bearded.Monads;

namespace GloomChars.Api.Authentication
{
    public class BearerTokenAuthHandler : AuthenticationHandler<BearerTokenAuthOptions>
    {
        readonly IAuthService _authSvc;

        public BearerTokenAuthHandler(
            IOptionsMonitor<BearerTokenAuthOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            IAuthService authSvc)
            : base(options, logger, encoder, clock)
        {
            _authSvc = authSvc;
        }

        private TokenUser CreateClaimsPrincipal(AuthenticatedUser user)
        {
            var role = user.IsSystemAdmin ? "SystemAdmin" : "None";

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString(), ClaimValueTypes.Integer),
                new Claim(BearerTokenDefaults.AccessTokenClaimName, user.AccessToken, ClaimValueTypes.String),
                new Claim(ClaimTypes.Role, role, ClaimValueTypes.String)
            };

            var identity = new ClaimsIdentity(claims, BearerTokenDefaults.AuthenticationScheme);

            return new TokenUser(user, identity);
        }

        private Either<AuthenticationTicket, string> CreateAuthenticationTicket(AuthenticatedUser user)
        {
            var principal = CreateClaimsPrincipal(user);
            return new AuthenticationTicket(principal, BearerTokenDefaults.AuthenticationScheme);
        }

        private Either<string, string> GetAccessToken()
        {
            //This returns a StringValues.Empty if the key is not found... nice work from MS
            string authorization = Request.Headers["Authorization"];

            //If no authorization header found, nothing to process further
            if (string.IsNullOrEmpty(authorization))
            {
                return Either<string, string>.CreateError("Authorization not found.");
            }

            if (!authorization.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            {
                return Either<string, string>.CreateError("Authorization header not a bearer token.");
            }

            string token = authorization.Substring("Bearer ".Length).Trim();
            return Either<string, string>.CreateSuccess(token);
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var authResult =
                from token in GetAccessToken()
                from authUser in _authSvc.GetAuthenticatedUser(token)
                from ticket in CreateAuthenticationTicket(authUser)                
                select ticket;

            return Task.FromResult(
                authResult.Unify(AuthenticateResult.Success, _ => AuthenticateResult.NoResult())
            );
        }
    }
}
