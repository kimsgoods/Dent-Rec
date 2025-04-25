using DentRec.Application.Interfaces;
using DentRec.Domain.Entities;
using Gridify;
using Gridify.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

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
            return await context.Set<T>().Where(x => !x.IsDeleted).FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<T?> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = context.Set<T>().Where(x => !x.IsDeleted && x.Id == id);

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.FirstOrDefaultAsync();
        }

        public async Task<T?> GetByIdAsync(int id, params Func<IQueryable<T>, IQueryable<T>>[] includes)
        {
            var query = context.Set<T>().Where(x => !x.IsDeleted && x.Id == id);

            foreach (var include in includes)
            {
                query = include(query);
            }

            return await query.FirstOrDefaultAsync();
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
                .GridifyAsync(gridifyQuery);

            return pagedResult;
        }
        public async Task<Paging<T>> GetPaginatedRecordsAsync(GridifyQuery gridifyQuery, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = context.Set<T>().Where(x => !x.IsDeleted);

            // Apply includes dynamically
            foreach (var include in includes)
            {
                query = query.Include(include);
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
