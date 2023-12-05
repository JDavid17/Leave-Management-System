using Microsoft.Extensions.DependencyInjection;

namespace LeaveManagement.Persistence;

public static class PersistenceServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services)
    {
        return services;
    }
}