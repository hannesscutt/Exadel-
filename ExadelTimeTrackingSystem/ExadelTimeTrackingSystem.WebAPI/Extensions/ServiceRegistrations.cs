namespace ExadelTimeTrackingSystem.WebAPI.Extensions
{
    using ExadelTimeTrackingSystem.BusinessLogic.Services;
    using ExadelTimeTrackingSystem.BusinessLogic.Services.Abstract;
    using Microsoft.Extensions.DependencyInjection;

    public static class ServiceRegistrations
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddSingleton<IProjectService, ProjectService>();
            services.AddSingleton<ITaskService, TaskService>();
            services.AddSingleton<IUserService, UserService>();

            return services;
        }
    }
}
