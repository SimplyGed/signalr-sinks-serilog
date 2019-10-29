using System;
using System.IO;
using Microsoft.AspNetCore.SignalR;
using Serilog.Core;
using Serilog.Events;

public class SignalR<THub> : ILogEventSink where THub : Hub<THub>, ILogHub
{
    private readonly IHubContext<THub> _hubContext;
    private readonly IFormatProvider _formatProvider;

    public SignalR(IHubContext<THub> hubContext, IFormatProvider formatProvider)
    {
        _hubContext = hubContext;
        _formatProvider = formatProvider;
    }

    public void Emit(LogEvent logEvent)
    {
        var message = logEvent.RenderMessage(_formatProvider);

        _hubContext.Clients.All.SendAsync("LogEvent", message);
    }
}
