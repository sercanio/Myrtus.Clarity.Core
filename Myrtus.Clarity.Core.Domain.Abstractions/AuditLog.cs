﻿namespace Myrtus.Clarity.Core.Domain.Abstractions
{
    public class AuditLog : Entity
    {
        public string User { get; set; }
        public string Action { get; set; }
        public string Entity { get; set; }
        public string EntityId { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public string Details { get; set; }
    }
}
