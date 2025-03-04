using Microsoft.EntityFrameworkCore;
using Infra.Persistance.Interfaces;
using Infra.Persistance.Entities;
using System.Linq.Expressions;

namespace Infra.Persistance
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        internal readonly DbContext _context;
        internal readonly DbSet<T> _dbSet;
        public Repository(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }
        public async ValueTask<List<T>?> GetAll(CancellationToken ct = default, Func<IQueryable<T>, IOrderedQueryable<T>>? OrderBy = null)
        {
            var query = _dbSet.AsQueryable();
            query = SortBy(query, OrderBy);
            return await query.ToListAsync(ct);
        }



        public async ValueTask<List<T>?> GetList(CancellationToken ct = default, Expression<Func<T, bool>>? Filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? OrderBy = null)
        {
            var query = _dbSet.AsQueryable();
            query = FilterQuery(query, Filter);
            query = SortBy(query, OrderBy);
            return await query.ToListAsync(ct);
        }

        public async ValueTask<T?> GetById(int ProvinceId, CancellationToken ct = default)
            => await _dbSet.FindAsync(ProvinceId, ct);

        public async ValueTask Add(T? province, CancellationToken ct)
        {
            if (province is not null)
                await _dbSet.AddAsync(province, ct);
        }

        public async ValueTask Update(T province, CancellationToken ct)
        {
            if (province is not null && province.Id > 0)
            {
                var existingProvince = await _dbSet.FindAsync(province.Id, ct);
                if (existingProvince != null)
                {
                    _context.Entry(existingProvince).CurrentValues.SetValues(province);
                }
            }
        }

        private static IQueryable<T> FilterQuery(IQueryable<T> query,
            Expression<Func<T, bool>>? Filter = null)
        {
            if (Filter is not null)
            {
                query = query.Where(Filter);
            }

            return query;
        }
        private static IQueryable<T> SortBy(IQueryable<T> query,
            Func<IQueryable<T>, IOrderedQueryable<T>>? OrderBy = null)
        {
            if (OrderBy is not null)
            {
                query = OrderBy(query);
            }
            return query;
        }
    }
}
