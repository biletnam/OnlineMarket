using Microsoft.AspNet.SignalR;
using OnlineMarket.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineMarket.Hubs
{
    public class AppHub : Hub
    {
        public void Send(string message)
        {
            Clients.All.addMessage(message);
        }
        

    }
}