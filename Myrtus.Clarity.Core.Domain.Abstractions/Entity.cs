namespace Myrtus.Clarity.Core.Domain.Abstractions;

public abstract class Entity<Tid>
{
  public Tid Id { get; init; }
  public DateTime CreatedOnUtc { get; private set; }
  public DateTime? UpdatedOnUtc { get; private set; }
  public DateTime? DeletedOnUtc { get; private set; }

  private readonly List<IDomainEvent> _domainEvents = new();

  protected Entity(Tid id)
  {
    Id = id;
    CreatedOnUtc = DateTime.UtcNow;
  }

  protected Entity()
  {
    CreatedOnUtc = DateTime.UtcNow;
  }

  public IReadOnlyList<IDomainEvent> GetDomainEvents()
  {
    return _domainEvents.ToList();
  }

  public void ClearDomainEvents()
  {
    _domainEvents.Clear();
  }

  public void RaiseDomainEvent(IDomainEvent domainEvent)
  {
    _domainEvents.Add(domainEvent);
  }

  public void MarkUpdated()
  {
    UpdatedOnUtc = DateTime.UtcNow;
  }

  public void MarkDeleted()
  {
    DeletedOnUtc = DateTime.UtcNow;
  }
}