using Goder.BL.DTO;
using Goder.BL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Post([FromBody] SolutionCreateDTO solution)
        {
            var email = HttpContext?.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            await _solutionService.AddSolution(solution, email);

            return Ok();
        }
    }
}
