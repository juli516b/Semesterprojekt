using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Semesterprojekt.API.Presentation.Models
{
    public class UserDtoForRegisterDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}