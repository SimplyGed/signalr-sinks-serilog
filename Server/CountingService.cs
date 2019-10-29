using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

public class CountingService : IHostedService, IDisposable
{
    private readonly IHubContext<ChatHub> _chathub;
    private readonly ILogger<CountingService> _logger;

    private Timer _timer;

    public CountingService(IHubContext<ChatHub> chathub, ILogger<CountingService> logger)
    {
        _chathub = chathub;
        _logger = logger;
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _timer = new Timer(SendMessage, null, TimeSpan.Zero, TimeSpan.FromSeconds(1));

        _logger.LogDebug("Starting...");

        return Task.CompletedTask;
    }

    private void SendMessage(object state)
    {
        _logger.LogDebug("SendMessage debug message");

        _chathub.Clients.All.SendAsync("SendToAll", "Server", DateTime.Now.ToString("yyyy-MM-dd HH:mm.ss.fff"));
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _timer.Change(Timeout.Infinite, 0);

        _logger.LogDebug("Stopping...");

        return Task.CompletedTask;
    }
}