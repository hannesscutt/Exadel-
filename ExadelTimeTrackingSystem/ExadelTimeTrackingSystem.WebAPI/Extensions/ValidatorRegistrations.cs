namespace ExadelTimeTrackingSystem.WebAPI.Extensions
{
    using ExadelTimeTrackingSystem.BusinessLogic.DTOs;
    using ExadelTimeTrackingSystem.BusinessLogic.Services;
    using ExadelTimeTrackingSystem.BusinessLogic.Services.Abstract;
    using ExadelTimeTrackingSystem.Data.Validators;
    using FluentValidation;
    using Microsoft.Extensions.DependencyInjection;

    public static class ValidatorRegistrations
    {
        public static IServiceCollection AddValidators(this IServiceCollection services)
        {
            services.AddTransient<IValidator<CreateProjectDTO>, CreateProjectDTOValidator>();
            return services;
        }
    }
}