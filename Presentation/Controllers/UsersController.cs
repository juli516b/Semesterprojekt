using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Semesterprojekt.API.Presentation.Models;
using Semesterprojekt.Core.Entites;
using Semesterprojekt.Core.Repositories;
using Semesterprojekt.Infrastructure.Helpers;
namespace Semesterprojekt.Presentation.Controllers
{
    [ApiController]
    [Route ("api/users")]
    public class UsersController : ControllerBase {
        private readonly IMapper _mapper;
        private readonly IUserRepository _repository;
        private readonly IConfiguration _configuration;

        public UsersController (IMapper mapper, IUserRepository repository, IConfiguration configuration) {
            _mapper = mapper;
            _repository = repository;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserDtoForRegisterDto userDtoForRegisterDto)
        {
            //Vigtigt at username ikke er casesensitive. Derfor skal username laves lowercase.
            userDtoForRegisterDto.Username = userDtoForRegisterDto.Username.ToLower();
            if (await _repository.UserExists(userDtoForRegisterDto.Username))
                return BadRequest("Username already exists");
            //Kun muligt at tilføje username til useren der skal laves, da vi skal hashe passworded først
            var userToCreate = _mapper.Map<User>(userDtoForRegisterDto);
            var createdUser = await _repository.Register(userToCreate, userDtoForRegisterDto.Username);
            return StatusCode(201);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDto userForLoginDto)
        {
            //Tjek om useren findes
            var userFromRepo = await _repository.Login(userForLoginDto.Username.ToLower(), userForLoginDto.Password);


            if (userFromRepo == null)
                return Unauthorized();

            //Tokennet indeholder to claims. Det ene er Userens ID og userens Username
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userFromRepo.Id.ToString()),
                new Claim(ClaimTypes.Name, userFromRepo.UserName)
            };

            //For at finde ud af tokennet er et valid token skal serveren signe tokennet. 
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Secret").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            //Fortæller noget om hvordan tokennet skal opbygges - altså payloaden.
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            //Handleren kan lave tokens, hvilket tokendescriptoren skal bruges til.
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            //Her returnere vi tokennet med Ok og skriver den ud til useren. 
            return Ok(new
            {
                token = tokenHandler.WriteToken(token)
            });
        }
    }
}