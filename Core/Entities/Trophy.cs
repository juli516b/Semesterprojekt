using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Semesterprojekt.Core.Entites 
{
    public class Trophy : EntityBase
    {
        public int Goal { get; set; }
        public string Description { get; set; }

        [MaxLength(30)]
        public string Name { get; set; }
        public ICollection<UserTrophy> UserTrophies { get; set; }
    }
}