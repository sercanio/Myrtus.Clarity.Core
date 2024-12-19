using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Myrtus.Clarity.Core.Infrastructure.SignalR.Hubs;

public class AuditLogHub : Hub
{
    [Authorize]
    public async Task SendAuditLog(string message)
    {
        await Clients.All.SendAsync("ReceiveAuditLog", message);
    }
}
