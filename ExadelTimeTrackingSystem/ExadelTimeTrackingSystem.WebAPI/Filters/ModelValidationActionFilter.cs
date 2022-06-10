namespace ExadelTimeTrackingSystem.WebAPI.Filters
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using FluentValidation;
    using Microsoft.AspNetCore.Mvc.Filters;

    public class ModelValidationActionFilter : IAsyncActionFilter
    {
        private readonly IValidatorFactory _validatorFactory;

        public ModelValidationActionFilter(IValidatorFactory validatorFactory) => _validatorFactory = validatorFactory;

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
                // skip null values
                if (value == null)
                {
                    continue;
                }

                var validator = _validatorFactory.GetValidator(value.GetType());

                // skip objects with no validators
                if (validator == null)
                {
                    continue;
                }

                // validate
                var validationContext = new ValidationContext<object>(value);
                var result = await validator.ValidateAsync(validationContext);

                // var result = await validator.ValidateAsync(value);

                // if it's valid, continue
                if (result.IsValid)
                {
                    continue;
                }

                // if there are errors, copy to the response dictonary
                var dict = new Dictionary<string, string>();

                foreach (var e in result.Errors)
                {
                    dict[e.PropertyName] = e.ErrorMessage;
                }

                allErrors.Add(key, dict);
            }

            if (allErrors.Any())
            {
                // Do anything you want here, if the validation failed.
                // For example, you can set context.Result to a new BadRequestResult()
                // or implement the Post-Request-Get pattern.
            }
            else
            {
                await next();
            }
        }
    }
}