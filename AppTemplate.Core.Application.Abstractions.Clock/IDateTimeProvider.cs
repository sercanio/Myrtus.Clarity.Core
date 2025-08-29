namespace AppTemplate.Core.Application.Abstractions.Clock;

public interface IDateTimeProvider
{
    DateTime UtcNow { get; }
}
