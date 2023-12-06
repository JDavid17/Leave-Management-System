using LeaveManagement.Application.Contracts.Persistence;
using LeaveManagement.Domain.Common;
using LeaveManagement.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagement.Persistence.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    protected readonly HrDatabaseContext Context;

    public GenericRepository(HrDatabaseContext context)
    {
        Context = context;
    }

    public async Task<IReadOnlyList<T>> GetAsync()
    {
        return await Context.Set<T>().AsNoTracking().ToListAsync();
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        return await Context.Set<T>().AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task CreateAsync(T entity)
    {
        await Context.AddAsync(entity);
        await Context.SaveChangesAsync();
    }

    public async Task UpdateAsync(T entity)
    {
        Context.Update(entity);
        await Context.SaveChangesAsync();
    }

    public async Task DeleteAsync(T entity)
    {
        Context.Remove(entity);
        await Context.SaveChangesAsync();
    }
}