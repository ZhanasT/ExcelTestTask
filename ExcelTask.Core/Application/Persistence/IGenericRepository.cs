using System.Linq.Expressions;

namespace ExcelTask.Core.Application.Persistence
{

    public interface IGenericRepository<T> where T : class
    {
        Task<bool> Any();

        Task<List<T>> FindAll();

        Task<List<T>> FindByCondition(Expression<Func<T, bool>> expression);

        Task<T> Create(T entity);

        Task AddRangeAsync(List<T> entities);

        void Update(T entity);

        void Delete(T entity);

        Task<List<T>> Get(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, string includeProperties = "");
    }
}
