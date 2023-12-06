using FluentValidation;
using LeaveManagement.Application.Contracts.Persistence;

namespace LeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType;

public class CreateLeaveTypeCommandValidator : AbstractValidator<CreateLeaveTypeCommand>
{
    private readonly ILeaveTypeRepository _leaveTypeRepository;

    public CreateLeaveTypeCommandValidator(ILeaveTypeRepository leaveTypeRepository)
    {
        _leaveTypeRepository = leaveTypeRepository;

        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("{PropertyName} is required")
            .NotNull()
            .MaximumLength(70).WithMessage("{PropertyName} must be fewer then 70 characters");

        RuleFor(p => p.DefaultDays)
            .GreaterThanOrEqualTo(1).WithMessage("{PropertyName} cannot be less than 1")
            .LessThanOrEqualTo(100).WithMessage("{PropertyName} cannot be greater than 100");

        RuleFor(p => p)
            .MustAsync(LeaveTypeNameUnique)
            .WithMessage("LeaveType with the same Name already exists");
    }

    private Task<bool> LeaveTypeNameUnique(CreateLeaveTypeCommand command, CancellationToken cancellationToken)
    {
        return _leaveTypeRepository.IsLeaveTypeUnique(command.Name);
    }
}