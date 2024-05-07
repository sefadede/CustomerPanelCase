using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Business.Chat.Hubs
{
    public class ChatHub : Hub
    {
        public ChatHub()
        {

        }
        public async Task SendMessage(string message, string recipientId)
        {
            // Gelen mesajı diğer istemcilere iletmek için "ReceiveMessage" metodunu çağır
            await Clients.All.SendAsync("ReceiveMessage", message, recipientId);
        }
    }
}
