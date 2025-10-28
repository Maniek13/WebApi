using Infrastructure.Hubs;
using Microsoft.AspNetCore.SignalR;
using Serilog.Core;
using Serilog.Events;
using System.Collections.Concurrent;

namespace Infrastructure.Loging;

public class SignalRSink : ILogEventSink, IDisposable
{
    private readonly IHubContext<LogsHub> _hubContext;
    private readonly ConcurrentQueue<string> _logMessages = [];
    private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);
    private bool _disposed;

    public SignalRSink(IHubContext<LogsHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public void Dispose()
    {
        _disposed = true;
        _semaphore.Dispose();
    }

    public void Emit(LogEvent logEvent)
    {
        if (_disposed)
        {
            Console.Error.WriteLine($"SignalRSink is disposed");
            return;
        }

        string message = $"[LOG][{logEvent.Level.ToString()}] {logEvent.RenderMessage()}";
        _logMessages.Enqueue(message);

        _ = SendAsync();
    }

    private async Task SendAsync()
    {
        if (!await _semaphore.WaitAsync(0))
            return;

        try
        {
            while(_logMessages.TryDequeue(out var message))
            {
                await _hubContext.Clients.All.SendAsync("ReceiveLog", message.ToString());
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"SignalRSink disabled due to error: {ex.Message}");
        }
        finally
        {
            _semaphore.Release();
        }
    }
}
