using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Myrtus.Clarity.Core.Application.Abstractions.Notification;
using Myrtus.Clarity.Core.Infrastructure.SignalR.Hubs;

namespace Myrtus.Clarity.Core.Infrastructure.Notification.Services
{
    public class NotificationService : INotificationService, IDisposable
    {
        private readonly IMongoCollection<Domain.Abstractions.Notification> _notifications;
        private readonly IHubContext<NotificationHub> _hubContext;
        private readonly MongoClient _client;
        private bool _disposed;

        public NotificationService(
            IConfiguration configuration,
            IHubContext<NotificationHub> hubContext)
        {
            _client = new MongoClient(configuration.GetConnectionString("MongoDb"));
            IMongoDatabase database = _client.GetDatabase(configuration["MongoDb:Database"]);
            _notifications = database.GetCollection<Domain.Abstractions.Notification>("Notifications");
            _hubContext = hubContext;
        }

        private static Domain.Abstractions.Notification CreateNotification(string message, string? userId = null)
        {
            return new Domain.Abstractions.Notification
            {
                Id = Guid.NewGuid(),
                Message = message,
                UserId = userId!,
                Timestamp = DateTime.UtcNow
            };
        }

        private async Task InsertNotificationAsync(Domain.Abstractions.Notification notification)
        {
            await _notifications.InsertOneAsync(notification);
        }

        public async Task SendNotificationAsync(string message)
        {
            Domain.Abstractions.Notification notification = CreateNotification(message);
            await InsertNotificationAsync(notification);
            await _hubContext.Clients.All.SendAsync("ReceiveNotification", notification);
        }

        public async Task SaveNotificationAsync(string message, string userId)
        {
            Domain.Abstractions.Notification notification = CreateNotification(message, userId);
            await InsertNotificationAsync(notification);
        }

        public async Task SendNotificationToUserAsync(string message, string userId)
        {
            Domain.Abstractions.Notification notification = CreateNotification(message, userId);
            await InsertNotificationAsync(notification);
            await _hubContext.Clients.User(userId).SendAsync("ReceiveNotification", notification);
        }

        public async Task<List<Domain.Abstractions.Notification>> GetNotificationsByUserIdAsync(string userId)
        {
            FilterDefinition<Domain.Abstractions.Notification> filter = Builders<Domain.Abstractions.Notification>.Filter.Eq(n => n.UserId, userId);
            return await _notifications.Find(filter).ToListAsync();
        }

        public async Task SendNotificationToUsersAsync(string message, IEnumerable<string> userIds)
        {
            List<Domain.Abstractions.Notification> notifications = [];
            foreach (string userId in userIds)
            {
                Domain.Abstractions.Notification notification = CreateNotification(message, userId);
                notifications.Add(notification);
            }

            await _notifications.InsertManyAsync(notifications);

            foreach (Domain.Abstractions.Notification notification in notifications)
            {
                await _hubContext.Clients.User(notification.UserId).SendAsync("ReceiveNotification", notification);
            }
        }

        public async Task<List<Domain.Abstractions.Notification>> GetNotificationsByUserIdsAsync(IEnumerable<string> userIds)
        {
            FilterDefinition<Domain.Abstractions.Notification> filter = Builders<Domain.Abstractions.Notification>.Filter.In(n => n.UserId, userIds);
            return await _notifications.Find(filter).ToListAsync();
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
