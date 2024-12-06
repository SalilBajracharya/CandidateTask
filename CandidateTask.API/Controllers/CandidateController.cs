using CandidateTask.Application.Segregation.Candidates.Command;
using FluentResults;
using FluentResults.Extensions.AspNetCore;
using Microsoft.AspNetCore.Mvc;

namespace CandidateTask.API.Controllers
{
    [ApiController]
    public class CandidateController : CustomApiControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<Result>> AddUpdateCandidate([FromBody] AddUpdateCandidate command)
        {
            var result = await Mediator.Send(command);
            return Ok(result.Successes.FirstOrDefault().Message);
        }
    }
}
