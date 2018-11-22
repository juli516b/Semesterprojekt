using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using nytEksamensprojekt.Entities;

namespace nytEksamensprojekt.Models
{
    public class UserDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int PhoneNumber { get; set; }
        public int ZipCode { get; set; }
        public ICollection<UserTrophy> UserTrophies { get; set; }
        public ICollection<TrainingSession> TraingSessions  { get; set; }
    }
}