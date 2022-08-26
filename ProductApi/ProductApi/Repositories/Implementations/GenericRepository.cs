using Microsoft.EntityFrameworkCore;
using ProductApi.Db;
using ProductApi.Repositories.Interfaces;
using System.Linq.Expressions;

namespace ProductApi.Repositories.Implementations
{
    public class GenericRepository<T> :IGenericRepository<T> where T : class
    {
        private readonly ProductContext _context;
        public GenericRepository(ProductContext context)
        {
            _context = context;
        }

        public async Task<T> Create(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<T> Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<List<T>> GetAll(Expression<Func<T, bool>> filter = default)
        {
            IQueryable<T> query = _context.Set<T>().AsNoTracking();
            if(filter is not null)
                query = query.Where(filter);
            return await query.ToListAsync();
        }

        public async Task<T> GetById(Expression<Func<T, bool>> filter = default)
        {
            IQueryable<T> query = _context.Set<T>().AsNoTracking();
            if (filter is not null)
                query = query.Where(filter);
            return await query.FirstOrDefaultAsync();
        }

        public async Task<T> Update(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
