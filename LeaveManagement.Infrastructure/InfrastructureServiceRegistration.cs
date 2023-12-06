using LeaveManagement.Application.Contracts.Email;
using LeaveManagement.Application.Contracts.Logging;
using LeaveManagement.Application.Models.Email;
using LeaveManagement.Infrastructure.EmailService;
using LeaveManagement.Infrastructure.LoggingService;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LeaveManagement.Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection ConfigurationInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));

        services.AddTransient<IEmailSender, EmailSender>();
        services.AddScoped(typeof(IAppLogger<>), typeof(LoggerService<>));

        return services;
    }
}