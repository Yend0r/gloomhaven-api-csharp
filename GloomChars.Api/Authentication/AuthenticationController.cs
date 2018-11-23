using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Bearded.Monads;
using GloomChars.Authentication.Interfaces;
using GloomChars.Api.Errors;
using GloomChars.Authentication.Models;

namespace GloomChars.Api.Authentication
{
    [Route("[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        readonly IAuthService _authSvc;
        readonly IUserManager _userManager;

        public AuthenticationController(IAuthService authSvc, IUserManager userManager)
        {
            _authSvc = authSvc;
            _userManager = userManager;
        }

        [HttpPost("login")]
        public ActionResult<LoginResponse> Login(LoginRequest loginRequest)
        {
            return _authSvc.Authenticate(loginRequest.Email, loginRequest.Password)
                           .Map(u => new LoginResponse(u))
                           .Unify<LoginResponse, string, ActionResult<LoginResponse>>(
                                r => r, 
                                e => new BadRequestError("Login failed.", e).ToActionResult()
                            );
        }

        [HttpDelete("logout")]
        [Authorize]
        public ActionResult Logout()
        {
            return _userManager.GetCurrentUser(this.User)
                   .Map(u => _authSvc.RevokeToken(u.AccessToken))
                   .Unify(_ => NoContent(), e => e.ToActionResult());
        }
    }
}
