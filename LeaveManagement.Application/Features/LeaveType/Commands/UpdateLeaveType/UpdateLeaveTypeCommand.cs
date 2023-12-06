using MediatR;

namespace LeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType;

public record UpdateLeaveTypeCommand(int Id, string Name, int DefaultDays) : IRequest<Unit>;