using Microsoft.AspNetCore.Mvc.Filters;

namespace API.EXception
{
    public class MyException : ExceptionFilterAttribute
    {
        private readonly ILogger<MyException> logger;

        public MyException(ILogger<MyException> logger)
        {
            this.logger = logger;
        }

        public override void OnException(ExceptionContext context)
        {
            logger.LogError(context.Exception, context.Exception.Message);

            base.OnException(context);
        }
    }
}