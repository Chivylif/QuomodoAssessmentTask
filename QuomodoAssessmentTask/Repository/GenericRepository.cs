using Microsoft.EntityFrameworkCore;
using QuomodoAssessmentTask.Data;
using System.Linq.Expressions;

namespace QuomodoAssessmentTask.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly QuomodoDbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(QuomodoDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<bool> Create(T entity)
        {
            await _dbSet.AddAsync(entity);
            bool res = await SaveChangesAsync();

            return res == true ? true : false;
        }

        public async Task<bool> Delete(T entity)
        {
            _dbSet.Remove(entity);
            bool res = await SaveChangesAsync();

            return res == true ? true : false;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetById(Expression<Func<T, bool>> where)
        {
            return await _dbSet.FirstOrDefaultAsync(where);
        }

        public async Task<bool> Update(T entity)
        {
            _dbSet.Update(entity);
            bool res = await SaveChangesAsync();

            return res == true ? true : false;
        }

        public async Task<bool> SaveChangesAsync()
        {
            int result = await _context.SaveChangesAsync();

            if (result < 0)
            {
                throw new Exception("Error saving changes to the database.");
            }

            return true;
        }
    }
}