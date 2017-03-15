using OnlineMarket.Filters;
using System.Web.Http;

namespace OnlineMarket
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            log4net.Config.XmlConfigurator.Configure();

            config.Filters.Add(new LogActionFilterAttribute());

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
