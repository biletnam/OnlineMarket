using Microsoft.Owin;
using Owin;
[assembly: OwinStartup(typeof(OnlineMarket.Startup))]

namespace OnlineMarket
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}