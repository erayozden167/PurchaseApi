using Microsoft.EntityFrameworkCore;
using PurchaseApi.Infrastructure.Data.Interfaces;
using System.Linq.Expressions;

namespace PurchaseApi.Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(DbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();  // Generic entity set'i oluştur bu sayede tek bir yapı ile birden fazla tabloya ulaşıbabilir.
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate) // koşullu sorgu bu sayede tek bir metot ile her türlü sorguyu gerçekleştirebiliriz, tablo farketmeksizin.
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
