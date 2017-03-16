using log4net;
using System.Web.Http.Filters;

namespace OnlineMarket.Filters
{
    public class LogExceptionFilterAttribute : ExceptionFilterAttribute
    {
        protected static readonly ILog _logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            var exception = actionExecutedContext.Exception;
            _logger.Error(exception);

            base.OnException(actionExecutedContext);
        }
    }
}