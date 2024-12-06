using CandidateTask.Application.Common.ErrorHandling;
using FluentResults.Extensions.AspNetCore;
using Microsoft.AspNetCore.Mvc;

namespace CandidateTask.API.Results
{
    public class FluentResultFilterProfile : DefaultAspNetCoreResultEndpointProfile
    {
        public override ActionResult TransformFailedResultToActionResult(FailedResultToActionResultTransformationContext context)
        {
            var result = context.Result;

            var error = result.Errors.FirstOrDefault(e => e is CustomError);
            if (error is CustomError er)
            {
                return new ObjectResult(er.GetProblemDetails())
                {
                    StatusCode = (int)er.GetStatusCode()
                };
            }
            return base.TransformFailedResultToActionResult(context);
        }
    }
}
