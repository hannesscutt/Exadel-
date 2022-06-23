namespace ExadelTimeTrackingSystem.WebAPI.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ExadelTimeTrackingSystem.BusinessLogic;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;

    public class ResolveErrorFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is OperationCanceledException)
            {
                context.Result = new BadRequestObjectResult(Constants.Validation.REQUEST_WAS_CANCELLED);
            }
        }
    }
}
