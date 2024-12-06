using System.ComponentModel.DataAnnotations;
using AutoMapper;
using CandidateTask.Application.Common.Interface;
using CandidateTask.Application.Common.Interface.Candidates;
using CandidateTask.Data.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CandidateTask.Application.Segregation.Candidates.Command
{
    public record AddUpdateCandidate : IRequest<Unit>
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public string? Phone { get; set; }
        public required string Email { get; set; }
        public string? LinkedInURL { get; set; }
        public string? GitURL { get; set; }
        public required string Comment { get; set; }
    }

    public class AddUpdateCandidationHandler : IRequestHandler<AddUpdateCandidate, Unit>
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

        public async Task<Unit> Handle(AddUpdateCandidate request, CancellationToken cancellationToken)
        {
            //check if email exists
            var existingCandidate = await _ctx.Candidates
                .FirstOrDefaultAsync(x => x.Email == request.Email, cancellationToken);

            //if email exists update
            if (existingCandidate is not null)
            {
                _mapper.Map(request, existingCandidate);
            }
            else
            {
                //if email is new create new entity
                var newCandidate = _mapper.Map<Candidate>(request);
                await _ctx.Candidates.AddAsync(newCandidate);
            }
            await _ctx.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
