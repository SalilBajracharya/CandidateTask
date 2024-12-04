using FluentValidation;

namespace CandidateTask.Application.Segregation.Candidates.Command
{
    public class CandidateValidation : AbstractValidator<AddUpdateCandidate>
    {
        public CandidateValidation()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First name is required");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name is required");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid Email format.");

            RuleFor(x => x.Comment)
                .NotEmpty().WithMessage("A small comment is required.");
        }
    }
}
