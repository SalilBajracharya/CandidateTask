using CandidateTask.Application.Common.Interface;
using CandidateTask.Application.Common.Interface.Candidates;
using CandidateTask.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CandidateTask.Infrastructure.Services
{
    public class CandidateService : ICandidateService
    {
        private readonly IApplicationDbContext _ctx;
        public CandidateService(IApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<IEnumerable<Candidate>> GetAll()
        {
            return await _ctx.Candidates.ToListAsync();
        }

        public async Task<Candidate> GetByEmail(string email)
        {
           return await _ctx.Candidates.Where(x => x.Email == email).FirstOrDefaultAsync();
        }
    }
}
