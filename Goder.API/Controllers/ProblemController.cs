using Goder.BL.DTO;
using Goder.BL.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;


namespace Goder.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProblemController : ControllerBase
    {
        private readonly ProblemService _problemService;

        public ProblemController(ProblemService problemService)
        {
            _problemService = problemService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProblemDTO>> Get(Guid id)
        {
            var email = HttpContext?.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            return Ok(await _problemService.GetProblem(id, email));
        }

        [HttpGet("{skip}/{take}")]
        public async Task<ActionResult<ICollection<ProblemSimplifiedDTO>>> Get(int skip, int take)
        {
            var email = HttpContext?.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            return Ok(await _problemService.GetProblems(skip, take, email));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(Guid id, [FromBody] ProblemDTO problem)
        {
            return Ok(await _problemService.UpdateProblem(id, problem));
        }

        [HttpPost("{userId}")]
        public async Task<ActionResult> Post(Guid userId, [FromBody] ProblemCreateDTO problem)
        {
            //return Ok(await _problemService.CreateProblem(problem, new Guid(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier))));
            return Ok(await _problemService.CreateProblem(problem, userId));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {

            return Ok(_problemService.DeleteProblem(id));
        }
    }
}