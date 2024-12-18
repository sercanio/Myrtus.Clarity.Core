namespace Myrtus.Clarity.Core.Application.Abstractions.Notification
{
    public interface INotificationService
    {
        Task SendNotificationAsync(string details);
        Task SendNotificationToUserAsync(string details, string userId);
        Task SaveNotificationAsync(string details, string userId);
        Task SendNotificationToUserGroupAsync(string details, string groupName);
        Task<List<Domain.Abstractions.Notification>> GetNotificationsByUserIdAsync(string userId);
    }
}
