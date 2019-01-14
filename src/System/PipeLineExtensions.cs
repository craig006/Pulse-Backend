using System.Security;
using Microsoft.AspNetCore.Mvc;
using ServeUp.System;

namespace ServeUp.System
{
    public static class PipeLineExtensions
    {
        public static IActionResult ToActionResult(this ExecutionContext context)
        {
            if(context.Success)
            {
                if(context.Output != null)
                {
                    return new OkObjectResult(context.Output);
                }
                else
                {
                    return new OkResult();
                }
            }

            var result = new ObjectResult(context.Exception);

            if(context.Exception is ConflictException)
            {
                result.StatusCode = 409;
            }
            else if(context.Exception is SecurityException)
            {
                result.StatusCode = 403;
            }
            else
            {
                result.StatusCode = 500;
            }

            return result;

        }
    }
}