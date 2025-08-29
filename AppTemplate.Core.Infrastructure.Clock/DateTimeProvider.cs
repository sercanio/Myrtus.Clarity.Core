using AppTemplate.Core.Application.Abstractions.Clock;

namespace AppTemplate.Core.Infrastructure.Clock;

public sealed class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}
