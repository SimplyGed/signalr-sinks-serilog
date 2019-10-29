using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

public class ChatHub : Hub<ChatHub>
{
    public Task SendToAll(string name, string message)
    {
        return Clients.All.SendToAll(name, message);
    }
}

public interface IChatHub
{
    Task SendToAll(string name, string message);
}