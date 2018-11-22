using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace nytEksamensprojekt.Entities
{
    public class Trophy
    {
        public int Id { get; set; }
        public int Goal { get; set; }
        public string Description { get; set; }

        [MaxLength(30)]
        public string Name { get; set; }
        public ICollection<UserTrophy> UserTrophies { get; set; }
    }
}