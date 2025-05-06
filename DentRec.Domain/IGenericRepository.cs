using DentRec.Domain.Entities;

namespace DentRec.Domain
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T?> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> ListAllAsync();
        void Add(T entity);
        void Update(T entity);
        void Remove(T entity);
        Task<bool> ExistsAsync(int id);
        Task<int> SaveAsync(T entity);
    }
}
