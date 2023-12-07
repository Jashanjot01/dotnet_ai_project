using System.Collections.Concurrent;
using Microsoft.AspNetCore.SignalR;

public class User
{

    public string Email { get; set; }
    public HashSet<string> ConnectionIds { get; set; }
}

public class ChatHub : Hub
{
    private static readonly ConcurrentDictionary<string, User> Users
        = new ConcurrentDictionary<string, User>();

    public async Task SendMessage(string user, string message, string receiver)
    {

        await Clients.All.SendAsync("ReceiveMessage", user, message, receiver);
    }
}