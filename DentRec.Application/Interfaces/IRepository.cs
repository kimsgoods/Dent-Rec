using DentRec.Domain.Entities;
using Gridify;

namespace DentRec.Application.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<T?> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> ListAllAsync();
        Task<Paging<T>> GetPaginatedRecords(GridifyQuery gridifyQuery);
        void Add(T entity);
        void Update(T entity);
        void Remove(T entity);
        Task<int> SaveAsync(T entity);
    }
}
