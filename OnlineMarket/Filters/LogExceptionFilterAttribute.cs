using System.Reflection;
using System.Web.Http.Filters;
using log4net;

namespace OnlineMarket.Filters
{
    public class LogExceptionFilterAttribute : ExceptionFilterAttribute
    {
        protected static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            var exception = actionExecutedContext.Exception;
            Logger.Error(exception);

            base.OnException(actionExecutedContext);
        }
    }
}