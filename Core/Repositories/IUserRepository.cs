using System;
using System.Collections.Generic;
using System.Linq;
using Semesterprojekt.Core.Entites;

namespace Semesterprojekt.Core.Repositories 
{
    public interface IUserRepository : IRepository<User>
    {
        User Authenticate (string username, string password);
        IEnumerable<User> GetAllUsers ();
        User GetById (int id);
        User Create (User user, string password);
        void Update (User user, string password = null);
        void Delete (int id);
        bool UserExists (int userId);
    }
}