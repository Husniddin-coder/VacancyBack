using System.Linq.Expressions;
using Vacancy.Domain.Commons;

namespace Vacancy.Data.IRepositories;

public interface IRepostory<T> where T : Auditable
{
    public Task<T> GetByIdAsync(int id);

    public Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> expression);

    public IQueryable<T> GetAllAsQueryable();

    public Task<T> CreateAsync(T entity);

    public Task<T> UpdateAsync(T entity);

    public Task<bool> DeleteAsync(int id);
}
