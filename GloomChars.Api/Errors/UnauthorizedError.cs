using System;
using Microsoft.AspNetCore.Mvc;

namespace GloomChars.Api.Errors
{
    public class UnauthorizedError : IApiError
    {
        public ActionResult ToActionResult()
        {
            return new UnauthorizedResult();
        }
    }
}
