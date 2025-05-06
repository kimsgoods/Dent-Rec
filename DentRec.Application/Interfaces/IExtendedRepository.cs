using DentRec.Domain;
using DentRec.Domain.Entities;
using Gridify;

namespace DentRec.Application.Interfaces
{
    public interface IExtendedRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        Task<Paging<T>> GetPaginatedRecordsAsync(GridifyQuery gridifyQuery);
        Task<Paging<T>> GetPaginatedRecordsAsync(GridifyQuery gridifyQuery, params Func<IQueryable<T>, IQueryable<T>>[] includes);
        Task<T?> GetByIdAsync(int id, params Func<IQueryable<T>, IQueryable<T>>[] includes);
    }
}
