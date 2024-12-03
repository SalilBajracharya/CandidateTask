using CandidateTask.Application.Common.Interface;
using CandidateTask.Data.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CandidateTask.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        private readonly IMediator _mediator;

        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options,
            IMediator mediator
            ) : base(options) 
        {
            _mediator = mediator;    
        }

        public DbSet<Candidate> Candidates => Set<Candidate>();
    }
}
