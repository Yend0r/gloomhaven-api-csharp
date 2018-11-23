using System;
using Microsoft.AspNetCore.Mvc;

namespace GloomChars.Api.Errors
{
    public interface IApiError
    {
        ActionResult ToActionResult();
    }
}
