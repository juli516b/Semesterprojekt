using System.Collections.Generic;

namespace nytEksamensprojekt.Entities
{
    public class TrainingSession
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public ICollection<UserTrainingSession> UserTrainingSessions { get; set; }
    }
}