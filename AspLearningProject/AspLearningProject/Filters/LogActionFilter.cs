using Microsoft.AspNetCore.Mvc.Filters;
using ILogger = Serilog.ILogger;

namespace AspLearningProject.Filters
{
    [AttributeUsage(AttributeTargets.Class)]
    public class LogAttribute : ActionFilterAttribute, IActionFilter
    {
        private readonly ILogger logger;
        private readonly bool logAction;

        public LogAttribute(ILogger logger, bool logAction = false)
        {
            this.logger = logger;
            this.logAction = logAction;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
           if (logAction)
           logger.Information($"Start Action:{context.ActionDescriptor.DisplayName}");
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if(logAction)
            logger.Information($"End Action:{context.ActionDescriptor.DisplayName}");
        }
    }
}
