using Factoring.Service.Core.Interfaces;
using Factoring.Service.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Factoring.Service.Infrastructure.Repositories;

public class Repository<T>(ApplicationDbContext dbContext) : IRepository<T>
    where T : class
{
    public async Task<T?> GetByIdAsync(Guid id)
    {
        return await dbContext.Set<T>().FindAsync(id);
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await dbContext.Set<T>().ToListAsync();
    }

    public async Task<T> AddAsync(T entity)
    {
        await dbContext.Set<T>().AddAsync(entity);
        return entity;
    }

    public void Remove(T entity)
    {
        dbContext.Set<T>().Remove(entity);
    }
}