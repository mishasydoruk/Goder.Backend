using Goder.BL.DTO;
using Goder.BL.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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
            return Ok(await _problemService.GetProblem(id));
        }

        [HttpGet("{skip/take}")]
        public async Task<ActionResult<ICollection<ProblemDTO>>> Get(int skip, int take)
        {
            return Ok(await _problemService.GetProblems(skip, take));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(Guid id, [FromBody] ProblemDTO problem)
        {
            return Ok(await _problemService.UpdateProblem(id, problem));
        }

        [HttpPut()]
        public async Task<ActionResult> Post([FromBody] ProblemDTO problem)
        {
            return Ok(await _problemService.CreateProblem(problem, new Guid(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier))));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {

            return Ok(_problemService.DeleteProblem(id));
        }
    }
}