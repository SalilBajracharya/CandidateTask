using CandidateTask.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CandidateTask.Application.Common.Interface
{
    public interface IApplicationDbContext
    {
        public DbSet<Candidate> Candidates { get; }
    }
}
