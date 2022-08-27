using System.Linq.Expressions;

namespace ProductStockApi.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<List<T>> GetAll(Expression<Func<T, bool>> filter = default);
        Task<T> GetById(Expression<Func<T, bool>> filter = default);
        Task<T> Create(T entity);
        Task<T> Delete(T entity);
        Task<T> Update(T entity);
    }
}
