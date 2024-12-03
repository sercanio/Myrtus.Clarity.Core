namespace Myrtus.Clarity.Core.Domain.Abstractions;

public class Notification
{
    public Guid Id { get; set; }
    public string UserId { get; set; }
    public string Message { get; set; }
    public DateTime Timestamp { get; set; }

    public Notification()
    {
        Id = Guid.NewGuid();
        UserId = string.Empty;
        Message = string.Empty;
        Timestamp = DateTime.UtcNow;
    }
}
