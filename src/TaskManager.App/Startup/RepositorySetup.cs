using TaskManager.Domain.Tasks;
using TaskManager.Domain.Users;
using TaskManager.Persistence.Tasks;
using TaskManager.Persistence.Users;

namespace App.Startup;

public static class RepositorySetup
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ITaskRepository, TaskRepository>();

        return services;
    }
}