using System;
using Microsoft.AspNetCore.Mvc;

namespace GloomChars.Api.Errors
{
    public class ApiError
    {
        public string Message { get; set; }
        public string Detail { get; set; }

        public ApiError(string message, string detail)
        {
            Message = message;
            Detail = detail;
        }

        public ApiError(string message) 
        { 
            Message = message;
        }
    }
}
