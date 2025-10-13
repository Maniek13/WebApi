using Infrastructure.Hubs;
using Microsoft.AspNetCore.SignalR;
using Serilog.Core;
using Serilog.Events;

namespace Infrastructure.Loging;

public class SignalRSink : ILogEventSink
{
    private readonly IHubContext<LogsHub> _hubContext;

    public SignalRSink(IHubContext<LogsHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public void Emit(LogEvent logEvent)
    {
        string message = $"[LOG][{logEvent.Level.ToString()}] {logEvent.RenderMessage()}";

        _ = _hubContext.Clients.All.SendAsync("ReceiveLog", message)
            .ContinueWith(task =>
            {
            });
    }
}
