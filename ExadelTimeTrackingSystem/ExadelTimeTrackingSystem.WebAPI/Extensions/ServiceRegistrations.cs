namespace ExadelTimeTrackingSystem.WebAPI.Extensions
{
    using EmailService;
    using ExadelTimeTrackingSystem.BusinessLogic.Services;
    using ExadelTimeTrackingSystem.BusinessLogic.Services.Abstract;
    using Hangfire;
    using Hangfire.SqlServer;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public static class ServiceRegistrations
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddSingleton<IProjectService, ProjectService>();
            services.AddSingleton<ITaskService, TaskService>();
            services.AddSingleton<IUserService, UserService>();
            services.AddScoped<IEmailSender, EmailSender>();

            return services;
        }
    }
}
