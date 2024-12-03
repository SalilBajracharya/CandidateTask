using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CandidateTask.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class CustomApiControllerBase : ControllerBase
    {
        private ISender _mediator = null!;

        protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();
    }
}
