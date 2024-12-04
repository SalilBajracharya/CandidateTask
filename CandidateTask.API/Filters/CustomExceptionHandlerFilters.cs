using CandidateTask.Application.Common.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CandidateTask.API.Filters
{
    public class CustomExceptionHandlerFilters : ExceptionFilterAttribute
    {
        private readonly IDictionary<Type, Action<ExceptionContext>> _exceptionHandlers;

        public CustomExceptionHandlerFilters()
        {
            //Register custom exception handlers
            _exceptionHandlers = new Dictionary<Type, Action<ExceptionContext>>
            {
                { typeof(CustomValidationException), HandleValidationException }
            };
        }

        public override void OnException(ExceptionContext context)
        {
            HandleException(context);
            base.OnException(context);
        }

        private void HandleException(ExceptionContext context)
        {
            Type type = context.Exception.GetType();
            if (_exceptionHandlers.ContainsKey(type))
            {
                _exceptionHandlers[type].Invoke(context);
                return;
            }

            if (!context.ModelState.IsValid)
            {
                HandleInvalidModelStateException(context);
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

            context.Result = new ObjectResult(details)
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };

            context.ExceptionHandled = true;
        }



        private void HandleValidationException(ExceptionContext context)
        {
            var exception = (CustomValidationException)context.Exception;

            var details = new ValidationProblemDetails(exception.Errors)
            {
                Title = "Validation Exception.",
            };

            context.Result = new BadRequestObjectResult(details);

            context.ExceptionHandled = true;
        }

        private void HandleInvalidModelStateException(ExceptionContext context)
        {
            var details = new ValidationProblemDetails(context.ModelState)
            {
                Title = "Invalid Model Exception.",
                Detail = context.Exception.Message
            };

            context.Result = new BadRequestObjectResult(details);

            context.ExceptionHandled = true;
        }
    }
}
