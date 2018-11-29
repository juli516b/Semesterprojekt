using System.Collections.Generic;

namespace Semesterprojekt.Core.Entites 
{
    public class TrainingSession : EntityBase
    {
        public string Description { get; set; }
        public ICollection<UserTrainingSession> UserTrainingSessions { get; set; }
    }
}