using CandidateTask.Application.Common.Interface;
using CandidateTask.Application.Common.Interface.Candidates;
using CandidateTask.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CandidateTask.Infrastructure.Services.Candidates
{
    public class CandidateService : ICandidateService
    {
        private readonly IApplicationDbContext _ctx;
        private readonly ILogger<CandidateService> _logger;
        public CandidateService(IApplicationDbContext ctx, ILogger<CandidateService> logger)
        {
            _ctx = ctx;
            _logger = logger;
        }

        public async Task Add(Candidate entity, CancellationToken cancellationToken)
        {
            _logger.LogError("Add Test....");
            await _ctx.Candidates.AddAsync(entity);
            await _ctx.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<Candidate>> GetAll()
        {
            return await _ctx.Candidates.ToListAsync();
        }

        public async Task<Candidate> GetByEmail(string email)
        {
            return await _ctx.Candidates.Where(x => x.Email == email).FirstOrDefaultAsync();
        }

        public async Task Update(Candidate updatedCandidate, CancellationToken cancellationToken)
        {
            _logger.LogError("Add Test....");

            _ctx.Candidates.Update(updatedCandidate);
            await _ctx.SaveChangesAsync(cancellationToken);
        }
    }
}
