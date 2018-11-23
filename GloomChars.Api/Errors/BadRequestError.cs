using System;
using Microsoft.AspNetCore.Mvc;

namespace GloomChars.Api.Errors
{
    public class BadRequestError : IApiError
    {
        public string Message { get; set; }
        public string Detail { get; set; }

        public BadRequestError(string message, string detail)
        {
            Message = message;
            Detail = detail;
        }

        public BadRequestError(string message) 
        { 
            Message = message;
        }

        public ActionResult ToActionResult()
        {
            return new BadRequestObjectResult(this);
        }
    }
}
