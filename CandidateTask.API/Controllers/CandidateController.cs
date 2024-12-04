using CandidateTask.Application.Segregation.Candidates.Command;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CandidateTask.API.Controllers
{
    [ApiController]
    public class CandidateController : CustomApiControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<Unit>> AddUpdateCandidate([FromBody] AddUpdateCandidate command)
        {
            return await Mediator.Send(command);
        }
    }
}
