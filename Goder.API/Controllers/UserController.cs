using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Goder.BL.DTO;
using Goder.BL.Services;

namespace Goder.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService contactsService)
        {
            _userService = contactsService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> Get(Guid id)
        {
            return Ok(await _userService.GetUser(id));
        }

        [HttpPut("{userId}/avatar")]
        public async Task<ActionResult> UpdateUserAvatar(Guid userId, IFormFile photo)
        {
            return Ok(await _userService.UpdateUserAvatar(userId, photo));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(Guid id, [FromBody] UserDTO user)
        {
            return Ok(await _userService.UpdateUser(id,user));
        }
    }
}
