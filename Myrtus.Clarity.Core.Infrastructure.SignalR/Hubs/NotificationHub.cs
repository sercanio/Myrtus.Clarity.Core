using Microsoft.AspNetCore.SignalR;

namespace Myrtus.Clarity.Core.Infrastructure.SignalR.Hubs;

public class NotificationHub : Hub
{
    public async Task SendNotification(string message)
    {
        await Clients.All.SendAsync("ReceiveNotification", message);
    }
}
