namespace ExadelTimeTrackingSystem.WebAPI.Filters
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using FluentValidation;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;

    public class ModelValidationActionFilter : IAsyncActionFilter
    {
        private readonly IValidatorFactory _validatorFactory;

        public ModelValidationActionFilter(IValidatorFactory validatorFactory)
        {
            _validatorFactory = validatorFactory;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var allErrors = new Dictionary<string, object>();

            // Short-circuit if there's nothing to validate
            if (context.ActionArguments.Count == 0)
            {
                await next();
                return;
            }

            foreach (var (key, value) in context.ActionArguments)
            {
                // Skip null values
                if (value == null)
                {
                    continue;
                }

                var validator = _validatorFactory.GetValidator(value.GetType());

                // Skip objects with no validators
                if (validator == null)
                {
                    continue;
                }

                // Validate
                var validationContext = new ValidationContext<object>(value);
                var result = await validator.ValidateAsync(validationContext);

                // Var result = await validator.ValidateAsync(value);

                // If it's valid, continue
                if (result.IsValid)
                {
                    continue;
                }

                // If there are errors, copy to the response dictonary
                var dict = new Dictionary<string, string>();
                dict = result.Errors.ToDictionary(e => e.PropertyName, e => e.ErrorMessage);

                allErrors.Add(key, dict);
            }

            if (allErrors.Any())
            {
                context.Result = new BadRequestObjectResult(allErrors);
            }
            else
            {
                await next();
            }
        }
    }
}