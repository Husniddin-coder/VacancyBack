using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Vacancy.Data.DbContexts;
using Vacancy.Data.IRepositories;
using Vacancy.Domain.Commons;
using Microsoft.Extensions.Options;

namespace Vacancy.Data.Repositories;

public class Repository<T> : IRepostory<T> where T : Auditable
{
    private readonly AppDbContext _context;

    public Repository(AppDbContext context)
    {
        _context = context;
    }

    public virtual async Task<T> CreateAsync(T entity)
    {
        var result = await _context.AddAsync(entity);

        return await _context.SaveChangesAsync() > 0 ? result.Entity : null;
    }

    public virtual async Task<T> UpdateAsync(T entity)
    {
        var updated = _context.Update(entity).Entity;
        await _context.SaveChangesAsync();

        return updated;
    }

    public virtual async Task<bool> DeleteAsync(int id)
    {
        _context.Remove(await GetByIdAsync(id));

        return await _context.SaveChangesAsync() > 0 ? true : false;
    }

    public virtual IQueryable<T> GetAllAsQueryable()
        => _context.Set<T>();

    public virtual async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> expression)
        => await _context.Set<T>().Where(expression).ToListAsync();

    public virtual async Task<T> GetByIdAsync(int id)
        => await _context
        .Set<T>()
        .FirstOrDefaultAsync(x => x.Id == id);
}
