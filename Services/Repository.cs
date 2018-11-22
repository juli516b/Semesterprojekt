using System.Collections.Generic;
using System.Linq;
using nytEksamensprojekt.Entities;
using nytEksamensprojekt.Models;

namespace nytEksamensprojekt.Services
{

    public class Repository : IRepository
    {
        private GoTrainDbContext _context;

        public Repository(GoTrainDbContext context)
        {
            _context = context;
        }
        public void AddUser(User user)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteTrophy(User user)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteUser(User user)
        {
            _context.Users.Remove(user);
            Save();
        }

        public IEnumerable<UserTrophy> GetTrophiesForUser(int userId)
        {
            var trophies = _context.UserTrophies.Where(x => x.UserId == userId).ToList();
            return trophies;
        }

        public User GetUser(int userId)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<User> GetUsersByZipCode(int zipCode)
        {
            var users = _context.Users.Select(x => x).Where(x => x.ZipCode == zipCode).ToList();
            return users;
        }

        public IEnumerable<User> GetUsersById(IEnumerable<int> userIds)
        {
            throw new System.NotImplementedException();
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void UpdateTrophyForUser(User user)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateUser(User user)
        {
            throw new System.NotImplementedException();
        }

        public bool UserExists(int userId)
        {
            var user = _context.Users.Where(x => x.Id == userId);
            if (user == null)
            {
                return false;
            }
            return true;
        }

        public IEnumerable<User> GetUsers()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<User> GetUsers(IEnumerable<int> userIds)
        {
            throw new System.NotImplementedException();
        }

        public bool AddTrainingSession(TrainingSession trainingSession)
        {
            _context.TrainingSessions.Add(trainingSession);
            return Save();
        }
    }
}