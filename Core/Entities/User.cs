using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Semesterprojekt.Core.Entites 
{
    public class User : EntityBase
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public int PhoneNumber { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        [MaxLength(4)]
        public int ZipCode { get; set; }
        public ICollection<UserTrophy> UserTrophies { get; set; }
        public ICollection<UserTrainingSession> UserTrainingSessions  { get; set; }
        
    }
}