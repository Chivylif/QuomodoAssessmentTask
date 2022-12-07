using System.Linq.Expressions;

namespace QuomodoAssessmentTask.Repository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<bool> Create(T entity);
        Task<bool> Update(T entity);
        Task<bool> Delete(T entity);
        Task<T> GetById(Expression<Func<T, bool>> where);
        Task<IEnumerable<T>> GetAll();
        Task<bool> SaveChangesAsync();
    }
}
                                         
