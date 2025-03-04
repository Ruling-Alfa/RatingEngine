using Infra.Persistance.Entities;
using System.Linq.Expressions;

namespace Infra.Persistance.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        ValueTask Add(T? Entity, CancellationToken ct = default);
        ValueTask<T?> GetById(int Id, CancellationToken ct = default);
        ValueTask<List<T>?> GetAll(CancellationToken ct = default, Func<IQueryable<T>, IOrderedQueryable<T>>? OrderBy = null);
        ValueTask<List<T>?> GetList(CancellationToken ct = default,Expression<Func<T, bool>>? Filter=null, Func<IQueryable<T>, IOrderedQueryable<T>>? OrderBy = null);
        ValueTask Update(T Entity, CancellationToken ct = default);
    }
}