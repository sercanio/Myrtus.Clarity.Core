using MongoDB.Driver;
using Newtonsoft.Json;
using Microsoft.AspNetCore.SignalR;
using Myrtus.Clarity.Core.Infrastructure.SignalR.Hubs;
using Microsoft.Extensions.Configuration;
using Myrtus.Clarity.Core.Application.Abstractions.Auditing;
using Myrtus.Clarity.Core.Domain.Abstractions;

namespace Myrtus.Clarity.Core.Infrastructure.Auditing.Services
{
    public class AuditLogService : IAuditLogService, IDisposable
    {
        private readonly IMongoCollection<AuditLog> _auditLogs;
        private readonly IHubContext<AuditLogHub> _hubContext;
        private readonly MongoClient _client;
        private bool _disposed;
        public AuditLogService(IConfiguration configuration, IHubContext<AuditLogHub> hubContext)
        {
            _client = new MongoClient(configuration.GetConnectionString("MongoDb"));
            IMongoDatabase database = _client.GetDatabase(configuration["MongoDb:Database"]);
            _auditLogs = database.GetCollection<AuditLog>("AuditLogs");
            _hubContext = hubContext;
        }
        public async Task LogAsync(AuditLog log)
        {
            await _auditLogs.InsertOneAsync(log);
            string message = JsonConvert.SerializeObject(log);
            await _hubContext.Clients.All.SendAsync("ReceiveAuditLog", message);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _client?.Dispose();
                }
                _disposed = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
