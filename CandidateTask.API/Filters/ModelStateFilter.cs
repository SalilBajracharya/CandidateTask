﻿using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CandidateTask.API.Filters
{
    public class ModelStateFilter : IAsyncActionFilter
    {
        private readonly ILogger<ModelStateFilter> _logger;

        public ModelStateFilter(ILogger<ModelStateFilter> logger) { 
            _logger = logger;
        }

        public async Task OnActionExecutionAsync(
            ActionExecutingContext context,
            ActionExecutionDelegate next
        )
        {
            if (!context.ModelState.IsValid)
            {
                var validationErrors = context
                    .ModelState.Where(m => m.Value.Errors.Any())
                    .Select(e => new
                    {
                        Property = e.Key,
                        Errors = e.Value.Errors.Select(me => me.ErrorMessage),
                    });

                // Log the validation errors
                foreach (var error in validationErrors)
                {
                    _logger.LogError("Validation error in property '{Property}': {Errors}",
                        error.Property, string.Join(", ", error.Errors));
                }

                var problemDetails = new ProblemDetails
                {
                    Title = "Validation Error",
                    Detail = validationErrors.First().Errors.First(),
                    Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                    Status = (int)HttpStatusCode.BadRequest,
                    Extensions = { { "invalidProperties", validationErrors } },
                };

                context.Result = new ObjectResult(problemDetails)
                {
                    StatusCode = problemDetails.Status,
                };

                return;
            }

            await next();
        }
    }
}
