using LeaveManagement.Application.Contracts.Logging;
using Microsoft.Extensions.Logging;

namespace LeaveManagement.Infrastructure.LoggingService;

public class LoggerService<T> : IAppLogger<T>
{
    private readonly ILogger<T> _logger;

    public LoggerService(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<T>();
    }

    public void LogInformation(string message, params object[] args)
    {
        _logger.LogInformation(message, args);
    }

    public void LogWarning(string message, params object[] args)
    {
        _logger.LogWarning(message, args);
    }

    public void LogError(string message, params object[] args)
    {
        _logger.LogError(message, args);
    }
}