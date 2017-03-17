using System.Reflection;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using log4net;

namespace OnlineMarket.Filters
{
    public class LogActionFilterAttribute : ActionFilterAttribute
    {
        protected static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public override void OnActionExecuting(HttpActionContext filterContext)
        {
            var message =
                $"Executing controller {filterContext.ActionDescriptor.ControllerDescriptor.ControllerName}, action {filterContext.ActionDescriptor.ActionName}";
            Logger.Info(message);
        }

        public override void OnActionExecuted(HttpActionExecutedContext filterContext)
        {
            var message =
                $"Finished executing controller {filterContext.ActionContext.ControllerContext.ControllerDescriptor.ControllerName}, action {filterContext.ActionContext.ActionDescriptor.ActionName}";
            Logger.Info(message);
        }
    }
}
