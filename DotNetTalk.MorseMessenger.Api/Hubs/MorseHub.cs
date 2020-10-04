using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace DotNetTalk.MorseMessenger.Api.Hubs
{
    public class MorseHub : Hub
    {
        public async Task Send(string message)
        {
            await Clients.All.SendAsync("broadcastMessage", message);
        }
    }
}