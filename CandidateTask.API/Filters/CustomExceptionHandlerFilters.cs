using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CandidateTask.API.Filters
{
    public class CustomExceptionHandlerFilters : ExceptionFilterAttribute
    {
        private readonly IDictionary<Type, Action<ExceptionContext>> _exceptionHandlers;
        private readonly ILogger<CustomExceptionHandlerFilters> _logger;
        public CustomExceptionHandlerFilters(ILogger<CustomExceptionHandlerFilters> logger)
        {
            //Register custom exception handlers
            _exceptionHandlers = new Dictionary<Type, Action<ExceptionContext>>
            {

            };
            _logger = logger;
        }

        public override void OnException(ExceptionContext context)
        {
            HandleException(context);
            base.OnException(context);
        }

        private void HandleException(ExceptionContext context)
        {
            Type type = context.Exception.GetType();
            if (_exceptionHandlers.TryGetValue(type, out Action<ExceptionContext> value))
            {
                value.Invoke(context);
                return;
            }

            // Handle all other exceptions with a status code of 500
            var details = new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Internal Server Error",
                Type = "Server Error",
                Detail = "Contact the Service Provider"
            };

            _logger.LogError(context.Exception.Message);

            context.Result = new ObjectResult(details)
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };

            context.ExceptionHandled = true;
        }
    }
}
