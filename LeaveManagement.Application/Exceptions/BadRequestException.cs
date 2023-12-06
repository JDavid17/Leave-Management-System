using FluentValidation.Results;

namespace LeaveManagement.Application.Exceptions;

public class BadRequestException : Exception
{
    public List<string> ValidationErrors { get; }

    public BadRequestException(string message) : base(message)
    {
        ValidationErrors = new();
    }

    public BadRequestException(string message, ValidationResult validationResult) : base(message)
    {
        ValidationErrors = new();
        foreach (var validationFailure in validationResult.Errors)
        {
            ValidationErrors.Add(validationFailure.ErrorMessage);
        }
    }
}