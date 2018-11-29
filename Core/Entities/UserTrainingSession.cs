using System.ComponentModel.DataAnnotations.Schema;

namespace Semesterprojekt.Core.Entites 
{
    public class UserTrainingSession
    {
        [ForeignKey("TrainingSession")]
        public int TrainingSessionId { get; set; }
        public TrainingSession TrainingSession { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }
    }
}