using Core.Dtos;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace Server.Hubs
{
    public class ChatHub : Hub
    {
        [HubMethodName("sendMessage")]
        public void SendMessage(MessageDto messageDto)
        {
            messageDto.ConnectionId = Context.ConnectionId;
            Clients.All.SendAsync("messages", messageDto);
        }

        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await base.OnDisconnectedAsync(exception);
        }
    }
}
