using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace CandidateTask.Application.Common.ErrorHandling
{
    public abstract class CustomError : FluentResults.Error
    {

        protected CustomError(string message) {
            Message = message;
        }

        public abstract HttpStatusCode GetStatusCode();
        public abstract string GetDetailsType();
        public abstract string GetDetailsTitle();

        //TODO : public virtual Dictionary<string, object> GetExtensions() => new Dictionary<string, 
        public virtual ProblemDetails GetProblemDetails()
        {
            return new ProblemDetails
            {
                Status = (int)GetStatusCode(),
                Detail = Message,
                Type = GetDetailsType(),
                Title = GetDetailsTitle()
          //      Extensions = GetExtensions(),
            };
        }
    }
}
