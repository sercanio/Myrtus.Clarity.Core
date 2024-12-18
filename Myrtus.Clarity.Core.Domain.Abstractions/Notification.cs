namespace Myrtus.Clarity.Core.Domain.Abstractions
{
    public class Notification : Entity
    {
        public string UserId { get; set; }
        public string User { get; set; }
        public string Action { get; set; }
        public string Entity { get; set; }
        public string EntityId { get; set; }
        public DateTime Timestamp { get; set; }
        public string Details { get; set; }
        public bool IsRead { get; set; }

        public Notification()
        {
            Id = Guid.NewGuid();
            UserId = string.Empty;
            User = string.Empty;
            Action = string.Empty;
            Entity = string.Empty;
            EntityId = string.Empty;
            Timestamp = DateTime.UtcNow;
            Details = string.Empty;
            IsRead = false;
        }
    }
}
