using CandidateTask.Application.Segregation.Candidates.Command;
using CandidateTask.Data.Entities;

namespace CandidateTask.Application.Common.Interface.Candidates
{
    public interface ICandidateService
    {
        Task<IEnumerable<Candidate>> GetAll();
        Task<Candidate> GetByEmail(string email);
        Task Add(Candidate newCandidate, CancellationToken cancellationToken);
        Task Update(Candidate updatedCandidate, CancellationToken cancellationToken);
    }
}
