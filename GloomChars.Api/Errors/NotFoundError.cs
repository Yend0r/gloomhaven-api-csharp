using System;
using Microsoft.AspNetCore.Mvc;

namespace GloomChars.Api.Errors
{
    public class NotFoundError : IApiError
    {
        public ActionResult ToActionResult()
        {
            return new NotFoundResult();
        }
    }
}
