using Goder.BL.DTO;
using Goder.BL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Goder.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class RegistratonController : ControllerBase
    {
        private RegistrationService _registrationService;

        public RegistratonController(RegistrationService registrationService)
        {
            _registrationService = registrationService;
        }

        [HttpPost]
        public async Task<ActionResult<UserDTO>> RegisterUser([FromBody] FirebaseUserDTO user)
        {
            return Ok(await _registrationService.RegisterUser(user));
        }
    }
}
