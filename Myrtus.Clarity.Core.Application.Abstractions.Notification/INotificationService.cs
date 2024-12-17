namespace Myrtus.Clarity.Core.Application.Abstractions.Notification
{
    public interface INotificationService
    {
        Task SendNotificationAsync(string message);
        Task SendNotificationToUserAsync(string message, string userId);
        Task SaveNotificationAsync(string message, string userId);
        Task<List<Domain.Abstractions.Notification>> GetNotificationsByUserIdAsync(string userId);
    }
}
