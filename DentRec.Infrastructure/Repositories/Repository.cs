using DentRec.Application.CRUD.Interfaces;
using DentRec.Domain.Entities;
using Gridify;
using Gridify.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace DentRec.Infrastructure.Repositories
{
    public class Repository<T>(ApplicationDbContext context) : IExtendedRepository<T> where T : BaseEntity
    {
        public void Add(T entity)
        {
            context.Set<T>().Add(entity);
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await context.Set<T>().Where(x => !x.IsDeleted).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<T?> GetByIdAsync(int id, params Func<IQueryable<T>, IQueryable<T>>[] includes)
        {
            var query = context.Set<T>().Where(x => !x.IsDeleted && x.Id == id);

            foreach (var include in includes)
            {
                query = include(query);
            }

            return await query.AsSplitQuery().FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyList<T>> ListAllAsync()
        {
            return await context.Set<T>().Where(x => !x.IsDeleted).ToListAsync();
        }

        public void Remove(T entity)
        {
            entity.IsDeleted = true;
            Update(entity);
        }

        public void Update(T entity)
        {
            context.Set<T>().Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
        }
        public async Task<int> SaveAsync(T entity)
        {
            var result = await context.SaveChangesAsync();
            return result > 0 ? entity.Id : 0;  // Return entity Id if the save was successful
        }

        public async Task<Paging<T>> GetPaginatedRecordsAsync(GridifyQuery gridifyQuery)
        {
            var pagedResult = await context.Set<T>()
                .Where(e => !e.IsDeleted)
                .AsNoTracking()
                .GridifyAsync(gridifyQuery);

            return pagedResult;
        }

        public async Task<Paging<T>> GetPaginatedRecordsAsync(GridifyQuery gridifyQuery, params Func<IQueryable<T>, IQueryable<T>>[] includes)
        {
            if (string.IsNullOrEmpty(gridifyQuery.OrderBy))
            {
                gridifyQuery.OrderBy = "Id"; // Default sort
            }
            var query = context.Set<T>().Where(x => !x.IsDeleted).AsSplitQuery().AsNoTracking();

            // Apply includes dynamically
            foreach (var include in includes)
            {
                query = include(query);
            }

            // Apply Gridify for filtering, sorting, and pagination
            return await query.GridifyAsync(gridifyQuery);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await context.Set<T>().Where(x => !x.IsDeleted).AnyAsync(x => x.Id == id);
        }


    }
}
