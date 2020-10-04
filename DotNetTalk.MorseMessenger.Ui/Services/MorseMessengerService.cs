using DotNetTalk.MorseMessenger.Common;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetTalk.MorseMessenger.Ui.Services
{
    public class MorseMessengerService
    {
        public Task SendMessage(Message message)
        {
            return Task.CompletedTask;
        }
    }
}
