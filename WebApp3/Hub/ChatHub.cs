using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace WebApp3
{
    public class ChatHub : Hub
    {
        public async Task Send(string user, string message, string dateTimePublishComment)
        {
            await Clients.All.SendAsync("Send", user, message, dateTimePublishComment);
        }
    }
}
