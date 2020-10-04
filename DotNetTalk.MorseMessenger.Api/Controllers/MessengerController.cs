using DotNetTalk.MorseMessenger.Api.Hubs;
using DotNetTalk.MorseMessenger.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace DotNetTalk.MorseMessenger.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MessengerController : ControllerBase
    {
        private readonly IHubContext<MorseHub> _hubContext;

        public MessengerController(IHubContext<MorseHub> morseHub)
        {
            _hubContext = morseHub;
        }

        [HttpPost("SendMessage")]
        public IActionResult SendMessage(Message message)
        {
            _hubContext.Clients.All.SendAsync("broadcastMessage", message.Body);
            return Ok();
        }
    }
}