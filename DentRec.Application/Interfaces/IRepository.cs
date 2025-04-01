using DentRec.Domain.Entities;
using Gridify;
using System.Linq.Expressions;

namespace DentRec.Application.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<T?> GetByIdAsync(int id);
        Task<T?> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes);
        Task<IReadOnlyList<T>> ListAllAsync();
        Task<Paging<T>> GetPaginatedRecordsAsync(GridifyQuery gridifyQuery);
        Task<Paging<T>> GetPaginatedRecordsAsync(GridifyQuery gridifyQuery, params Expression<Func<T, object>>[] includes);
        void Add(T entity);
        void Update(T entity);
        void Remove(T entity);
        Task<int> SaveAsync(T entity);
        Task<bool> ExistsAsync(int id);
    }
}
