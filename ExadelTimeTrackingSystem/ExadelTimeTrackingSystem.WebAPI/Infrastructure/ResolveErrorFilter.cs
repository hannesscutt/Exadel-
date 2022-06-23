namespace ExadelTimeTrackingSystem.WebAPI.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;

    public class ResolveErrorFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.HttpContext.RequestAborted.IsCancellationRequested)
            {
                context.Result = new BadRequestObjectResult("test");
            }
            else
            {
                await next();
            }
        }
    }
}
