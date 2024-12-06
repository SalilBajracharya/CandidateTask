using CandidateTask.Data.Entities;

namespace CandidateTask.Application.Common.Interface.Candidates
{
    public interface ICandidateService
    {
        Task<IEnumerable<Candidate>> GetAll();
        Task<Candidate> GetByEmail(string email);
    }
}
