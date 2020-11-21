using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


using Goder.DAL.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

using Goder.BL.Services;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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
        // GET: api/<UserController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public string Get(Guid id)
        {
            User user = _userService.GetUser(id);
            if (user == null)
                throw new Exception("User not found");
            string jsonString = JsonSerializer.Serialize(user);
            return jsonString;
        }


        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public void Put(Guid id, [FromBody] User user, string value)
        {
            _userService.UpdateUser(id, user);
        }

        
    }
}
