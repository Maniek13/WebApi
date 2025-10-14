using Microsoft.AspNetCore.SignalR;

namespace Infrastructure.Hubs;

public class ChatHub : Hub
{
    public override Task OnConnectedAsync()
    {
        return base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        return base.OnDisconnectedAsync(exception);
    }

    public async Task SendMessage(string senderName, string message, string? receiverId = null)
    {
        var msg = $"{senderName}: {message}";

        if (string.IsNullOrWhiteSpace(receiverId))
            await Clients.All.SendAsync("ReceiveMessage", msg);
        else
            await Clients.User(receiverId!).SendAsync("ReceiveMessage", msg);
    }
}
