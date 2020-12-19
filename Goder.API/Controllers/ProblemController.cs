using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Goder.DAL.Models;
using System.Text.Json;
using System.Text.Json.Serialization;
using Goder.BL.DTO;
using Goder.BL.Services;


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


        // GET api/<ProblemController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProblemDTO>> Get(Guid id)
        {
            return Ok(await _problemService.GetProblem(id));
        }
        
        [HttpGet("{skip/take}")]
        public async Task<ActionResult<ICollection<ProblemDTO>>> Get(int skip ,int take)
        {
            return Ok(await _problemService.GetProblems(skip, take));
        }


        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(Guid id, [FromBody] ProblemDTO problem)
        {
            return Ok(await _problemService.UpdateProblem(id, problem));
        }
        
        // POST api/<UserController>/
        [HttpPut()]
        public async Task<ActionResult> Post([FromBody] ProblemDTO problem)
        {
            return Ok(await _problemService.CreateProblem(problem,new Guid(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier))));
        }
        
        // Delete api/<UserController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            
            return Ok(_problemService.DeleteProblem(id));
        }

        
    }
}