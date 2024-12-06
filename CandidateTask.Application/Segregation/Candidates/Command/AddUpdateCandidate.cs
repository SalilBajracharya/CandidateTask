using System.ComponentModel.DataAnnotations;
using AutoMapper;
using CandidateTask.Application.Common.Interface;
using CandidateTask.Application.Common.Interface.Candidates;
using CandidateTask.Data.Entities;
using FluentResults;
using MediatR;

namespace CandidateTask.Application.Segregation.Candidates.Command
{
    public record AddUpdateCandidate : IRequest<Result>
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string Phone { get; set; }
        [Required]
        [EmailAddress]
        public required string Email { get; set; }
        public string LinkedInURL { get; set; }
        public string GitURL { get; set; }
        [Required]
        public string Comment { get; set; }
    }

    public class AddUpdateCandidationHandler : IRequestHandler<AddUpdateCandidate, Result>
    {
        private readonly IApplicationDbContext _ctx;
        private readonly IMapper _mapper;
        private readonly ICandidateService _candidateService;
        public AddUpdateCandidationHandler(IApplicationDbContext ctx, IMapper mapper, ICandidateService candidateService)
        {
            _ctx = ctx;
            _mapper = mapper;
            _candidateService = candidateService;
        }

        public async Task<Result> Handle(AddUpdateCandidate request, CancellationToken cancellationToken)
        {
            //check if email exists
            var existingCandidate = await _candidateService.GetByEmail(request.Email);

            //if email exists update
            if (existingCandidate is not null)
            {
                _mapper.Map(request, existingCandidate);
                await _candidateService.Update(existingCandidate, cancellationToken);
                return Result.Ok().WithSuccess("Candidate updated successfully.");
            }
            else
            {
                //if email is new create new entity
                var newCandidate = _mapper.Map<Candidate>(request);
                await _candidateService.Add(newCandidate, cancellationToken);
                return Result.Ok().WithSuccess("Candidate added successfully.");
            }
        }
    }
}
