using AutoMapper;
using CandidateTask.Application.Common.Interface;
using CandidateTask.Data.Entities;
using MediatR;

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
        public AddUpdateCandidationHandler(IApplicationDbContext ctx, IMapper mapper)
        {
            _ctx = ctx;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(AddUpdateCandidate request, CancellationToken cancellationToken)
        {
            var mappedCandidate = _mapper.Map<Candidate>(request);
            await _ctx.Candidates.AddAsync(mappedCandidate);
            await _ctx.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
