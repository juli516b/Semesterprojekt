using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace nytEksamensprojekt.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int PhoneNumber { get; set; }
        [MaxLength(4)]
        public int ZipCode { get; set; }
        public ICollection<UserTrophy> UserTrophies { get; set; }
        public ICollection<UserTrainingSession> UserTrainingSessions  { get; set; }
        
    }
}