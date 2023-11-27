using Microsoft.AspNetCore.SignalR;
using Serilog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CrealutionRealtimeServer.WebApi.Hubs
{
    public class AuthorizeHub : Hub
    {
        private readonly List<string> _connectedUsers;
        private readonly ILogger _logger;

        public AuthorizeHub(ILogger logger)
        {
            _connectedUsers = new List<string>();
            _logger = logger;
        }

        public override async Task OnConnectedAsync()
        {
            var id = Context.ConnectionId;

            _connectedUsers.Add(id);

            _logger.Information($"User {id} connected!");

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var id = Context.ConnectionId;

            _connectedUsers.Remove(id);

            _logger.Information($"User {id} disconnected!");

            await base.OnDisconnectedAsync(exception);
        }
    }
}