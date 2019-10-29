using Microsoft.AspNetCore.SignalR;
using Serilog;
using Serilog.Configuration;

public static class SignalRSinksExtensions
{
    const string DefaultOutputTemplate = "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}";

    public static LoggerConfiguration SignalRLogger<THub>(this LoggerSinkConfiguration sinkConfiguration, IHubContext<THub> hub)
        where THub: Hub<THub>, ILogHub
    {
        return sinkConfiguration.Sink(new SignalR<THub>(hub, null));
    }
}