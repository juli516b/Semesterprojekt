using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Semesterprojekt.Core.Entites;
using Semesterprojekt.Core.Repositories;

namespace Semesterprojekt.Presentation.Controllers 
{
    [Route ("api/users")]
    public class UsersController : Controller {
        private readonly IMapper _mapper;
        private readonly IUserRepository _repository;

        public UsersController (IMapper mapper, IUserRepository repository) {
            _mapper = mapper;
            _repository = repository;
        }

        // GET api/users
        [HttpGet ("")]
        public ActionResult<IEnumerable<string>> Gets () {
            return new string[] { "value1", "value2" };
        }

        // GET api/users/5
        [HttpGet ("{id}")]
        public IActionResult GetById (int userId) 
        {
            return Ok();
        }

        // POST api/user
        [HttpPost ("register")]
        public void Post ([FromBody] string value) { }

        // PUT api/user/5
        [HttpPut ("{id}")]
        public void Put (int id, [FromBody] string value) { }

        // DELETE api/user/5
        [HttpDelete ("{id}")]
        public void DeleteById (int id) { }
    }
}