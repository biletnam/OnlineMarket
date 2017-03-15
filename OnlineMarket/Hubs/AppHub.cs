using Microsoft.AspNet.SignalR;

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