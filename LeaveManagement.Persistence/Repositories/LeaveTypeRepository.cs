using LeaveManagement.Application.Contracts.Persistence;
using LeaveManagement.Domain;
using LeaveManagement.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagement.Persistence.Repositories;

public class LeaveTypeRepository : GenericRepository<LeaveType>, ILeaveTypeRepository
{
    public LeaveTypeRepository(HrDatabaseContext context) : base(context)
    {
    }

    public async Task<bool> IsLeaveTypeUnique(string name)
    {
        var isUnique = !await Context.Set<LeaveType>().AnyAsync(lt => lt.Name == name);
        return isUnique;
    }

    public async Task<bool> IsLeaveTypeUnique(string name, int id)
    {
        var isUnique = !await Context.Set<LeaveType>().AnyAsync(lt => lt.Name == name && lt.Id != id);
        return isUnique;
    }
}