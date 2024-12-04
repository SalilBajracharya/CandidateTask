using System.Reflection;
using CandidateTask.Application.Common.Interface;
using CandidateTask.Data.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CandidateTask.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext() { }
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options
            ) : base(options) 
        {
        }

        public DbSet<Candidate> Candidates => Set<Candidate>();

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }
    }
}
