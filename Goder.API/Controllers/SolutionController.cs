using Goder.BL.DTO;
using Goder.BL.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Goder.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SolutionController : ControllerBase
    {
        private readonly SolutionService _solutionService;

        public SolutionController(SolutionService contactsService)
        {
            _solutionService = contactsService;
        }

        [HttpPost("{id}")]
        public async Task<ActionResult> Post([FromBody] SolutionCreateDTO solution)
        {
            await _solutionService.AddSolution(solution);

            return Ok();
        }
    }
}
