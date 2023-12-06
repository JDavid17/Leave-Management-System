using LeaveManagement.Application.Contracts.Persistence;
using LeaveManagement.Domain;
using LeaveManagement.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagement.Persistence.Repositories;

public class LeaveRequestRepository : GenericRepository<LeaveRequest>, ILeaveRequestRepository
{
    public LeaveRequestRepository(HrDatabaseContext context) : base(context)
    {
    }

    public async Task<LeaveRequest?> GetLeaveRequestsWithDetails(int id)
    {
        return await Context.LeaveRequests.Include(lr => lr.LeaveType)
            .FirstOrDefaultAsync(lr => lr.Id == id);
    }

    public async Task<List<LeaveRequest>> GetLeaveRequestsWithDetails()
    {
        return await Context.LeaveRequests.Include(lr => lr.LeaveType).ToListAsync();
    }

    public async Task<List<LeaveRequest>> GetLeaveRequestsWithDetails(string userId)
    {
        return await Context.LeaveRequests.Where(lr => lr.RequestingEmployeeId.Equals(userId))
            .Include(lr => lr.LeaveType)
            .ToListAsync();
    }
}