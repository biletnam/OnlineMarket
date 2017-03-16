
using log4net;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace OnlineMarket.Filters
{
    public class LogActionFilterAttribute : ActionFilterAttribute
    {
        protected static readonly ILog _logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public override void OnActionExecuting(HttpActionContext filterContext)
        {
            var message = $"Executing controller {filterContext.ActionDescriptor.ControllerDescriptor.ControllerName}, action {filterContext.ActionDescriptor.ActionName}";
            _logger.Info(message);
        }

        public override void OnActionExecuted(HttpActionExecutedContext filterContext)
        {
            var message = $"Finished executing controller { filterContext.ActionContext.ControllerContext.ControllerDescriptor.ControllerName}, action {filterContext.ActionContext.ActionDescriptor.ActionName}";
            _logger.Info(message);
        }
    }
}
