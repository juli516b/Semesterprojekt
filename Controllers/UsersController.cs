using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using nytEksamensprojekt.Models;
using nytEksamensprojekt.Services;

namespace nytEksamensprojekt.Controllers
{
    [Route("api/users")]
    public class UsersController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IRepository _repository;

        public UsersController(IMapper mapper, IRepository repository) 
        { 
            _mapper = mapper;
            _repository = repository;
        }

        // GET api/users
        [HttpGet("")]
        public ActionResult<IEnumerable<string>> Gets()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/users/5
        [HttpGet("{id}")]
        public IActionResult GetById(int userId)
        {
            if (_repository.UserExists(userId) == true)
            {
                return Ok(_repository.GetUser(userId));
            }
            return NotFound();
        }

        // POST api/user
        [HttpPost("")]
        public void Post([FromBody] string value) { }

        // PUT api/user/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value) { }

        // DELETE api/user/5
        [HttpDelete("{id}")]
        public void DeleteById(int id) { }
    }
}