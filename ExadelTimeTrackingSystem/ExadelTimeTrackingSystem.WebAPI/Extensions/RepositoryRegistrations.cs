﻿namespace ExadelTimeTrackingSystem.WebAPI.Extensions
{
    using ExadelTimeTrackingSystem.Data.Repositories;
    using ExadelTimeTrackingSystem.Data.Repositories.Abstract;
    using Microsoft.Extensions.DependencyInjection;

    public static class RepositoryRegistrations
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddSingleton<IProjectRepository, ProjectRepository>();
            services.AddSingleton<ITaskRepository, TaskRepository>();
            services.AddSingleton<IUserRepository, UserRepository>();
            return services;
        }
    }
}
