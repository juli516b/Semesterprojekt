using System.ComponentModel.DataAnnotations.Schema;

namespace Semesterprojekt.Core.Entites 
{
    public class UserTrophy
    {
        [ForeignKey("User")]
        public int UserId { get; set; }
        [ForeignKey("Trophy")]
        public int TrophyId { get; set; }
        public User User { get; set; }
        public Trophy Trophy { get; set; }
        public int Counter { get; set; }
    }
}