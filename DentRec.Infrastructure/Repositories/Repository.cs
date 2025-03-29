using DentRec.Application.Interfaces;
using DentRec.Domain.Entities;
using Gridify;
using Gridify.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace DentRec.Infrastructure.Repositories
{
    public class Repository<T>(ApplicationDbContext context) : IRepository<T> where T : BaseEntity
    {
        public void Add(T entity)
        {
            context.Set<T>().Add(entity);
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await context.Set<T>().FindAsync(id);
        }

        public async Task<IReadOnlyList<T>> ListAllAsync()
        {
            return await context.Set<T>().ToListAsync();
        }

        public void Remove(T entity)
        {
            context.Set<T>().Remove(entity);
        }

        public void Update(T entity)
        {
            context.Set<T>().Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
        }
        public async Task<int> SaveAsync(T entity)
        {
            await context.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<Paging<T>> GetPaginatedRecords(GridifyQuery gridifyQuery)
        {
            var pagedResult = await context.Set<T>().GridifyAsync(gridifyQuery);
            return pagedResult;
        }
    }
}
