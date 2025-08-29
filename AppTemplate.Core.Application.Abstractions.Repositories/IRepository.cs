using Myrtus.Clarity.Core.Domain.Abstractions;
using Myrtus.Clarity.Core.Infrastructure.Dynamic;
using Myrtus.Clarity.Core.Infrastructure.Pagination;
using System.Linq.Expressions;

namespace Myrtus.Clarity.Core.Application.Abstractions.Repositories;

public interface IRepository<TEntity, TKey>
    where TEntity : Entity<TKey>
{
  Task<TEntity?> GetAsync(
      Expression<Func<TEntity, bool>> predicate,
      bool includeSoftDeleted = false,
      Func<IQueryable<TEntity>, IQueryable<TEntity>>? include = null,
      bool asNoTracking = true,
      CancellationToken cancellationToken = default);

  Task<PaginatedList<TEntity>> GetAllAsync(
      int pageIndex = 0,
      int pageSize = 10,
      Expression<Func<TEntity, bool>>? predicate = null,
      bool includeSoftDeleted = false,
      Func<IQueryable<TEntity>, IQueryable<TEntity>>? include = null,
      bool asNoTracking = true,
      CancellationToken cancellationToken = default);

  Task<PaginatedList<TEntity>> GetAllDynamicAsync(
      DynamicQuery dynamicQuery,
      int pageIndex = 0,
      int pageSize = 10,
      bool includeSoftDeleted = false,
      Func<IQueryable<TEntity>, IQueryable<TEntity>>? include = null,
      bool asNoTracking = true,
      CancellationToken cancellationToken = default);

  Task<bool> ExistsAsync(
      Expression<Func<TEntity, bool>> predicate,
      bool includeSoftDeleted = false,
      bool asNoTracking = true,
      CancellationToken cancellationToken = default);

  Task AddAsync(TEntity entity);
  void Update(TEntity entity);
  void Delete(TEntity entity, bool isSoftDelete = true);
}