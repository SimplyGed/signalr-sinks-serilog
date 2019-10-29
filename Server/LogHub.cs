using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

public class LogHub : Hub<LogHub>, ILogHub
{
    public async Task LogEvent(string message)
    {
        await Clients.All.LogEvent(message);
    }
}

public interface ILogHub
{
    Task LogEvent(string message);
}