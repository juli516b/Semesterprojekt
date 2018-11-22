using System.Collections.Generic;
using nytEksamensprojekt.Entities;

namespace nytEksamensprojekt.Services
{
    public interface IRepository
    {
        IEnumerable<User> GetUsers();
        User GetUser(int userId);
        IEnumerable<User> GetUsersByZipCode(int zipCode);
        void AddUser(User user);
        void DeleteUser(User user);
        void UpdateUser(User user);
        bool UserExists(int userId);
        IEnumerable<UserTrophy> GetTrophiesForUser(int userId);
        bool AddTrainingSession(TrainingSession trainingSession);
        void UpdateTrophyForUser(User user);
        bool Save();
    }
}