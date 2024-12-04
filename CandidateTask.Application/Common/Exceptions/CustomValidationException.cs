using System.ComponentModel;
using FluentValidation.Results;

namespace CandidateTask.Application.Common.Exceptions
{
    public class CustomValidationException : Exception
    {
        public CustomValidationException() 
            : base("One or more validation failures have occured.") 
        {
            Errors = new Dictionary<string, string[]>();
        }

        public CustomValidationException(IEnumerable<ValidationFailure> failures)
            : this()
        {
            Errors = failures
                .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
                .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
        }

        public IDictionary<string, string[]> Errors { get; } 
    }
}
