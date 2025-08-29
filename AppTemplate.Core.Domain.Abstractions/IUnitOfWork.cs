namespace AppTemplate.Core.Domain.Abstractions;

public interface IUnitOfWork
{
  Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
  void ClearChangeTracker();
}
