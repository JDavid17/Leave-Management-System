using LeaveManagement.Application.Contracts.Persistence;
using LeaveManagement.Domain;
using LeaveManagement.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagement.Persistence.Repositories;

public class LeaveAllocationRepository : GenericRepository<LeaveAllocation>, ILeaveAllocationRepository
{
    public LeaveAllocationRepository(HrDatabaseContext context) : base(context)
    {
    }

    public async Task<LeaveAllocation?> GetLeaveAllocationWithDetails(int id)
    {
        return await Context.LeaveAllocations.Include(la => la.LeaveType)
            .FirstOrDefaultAsync(la => la.Id == id);
    }

    public async Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetails()
    {
        return await Context.LeaveAllocations.Include(la => la.LeaveType).ToListAsync();
    }

    public async Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetails(string userId)
    {
        return await Context.LeaveAllocations.Include(la => la.LeaveType)
            .Where(la => la.EmployeeId.Equals(userId)).ToListAsync();
    }

    public Task<bool> AllocationExists(string userId, int leaveTypeId, int period)
    {
        return Context.LeaveAllocations.AnyAsync(la =>
            la.EmployeeId.Equals(userId) && la.LeaveTypeId == leaveTypeId && la.Period == period);
    }

    public async Task AddAllocations(List<LeaveAllocation> allocations)
    {
        await Context.LeaveAllocations.AddRangeAsync(allocations);
        await Context.SaveChangesAsync();
    }

    public async Task<LeaveAllocation?> GetUserAllocation(string userId, int leaveTypeId)
    {
        return await Context.LeaveAllocations.FirstOrDefaultAsync(la =>
            la.EmployeeId.Equals(userId) && la.LeaveTypeId == leaveTypeId);
    }
}