using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Semesterprojekt.Core.Entites;
using Semesterprojekt.Core.Repositories;
using Semesterprojekt.Infrastructure.Helpers;
using Semesterprojekt.Persistence;

namespace Semesterprojekt.Persistence.Repositories {
    public class UserRepository : Repository<User>, IUserRepository {
        public UserRepository (GoTrainDbContext context) : base (context) { }

        public GoTrainDbContext GoTrainDbContext {
            get { return Context as GoTrainDbContext; }
        }

        public User Authenticate (string username, string password) {
            if (string.IsNullOrEmpty (username) || string.IsNullOrEmpty (password))
                return null;
            var user = GoTrainDbContext.Users.SingleOrDefault (x => x.UserName == username);
            if (user == null)
                return null;
            if (!PasswordHelper.VerifyPasswordHash (password, user.PasswordHash, user.PasswordSalt))
                return null;
            return user;
        }
        public User Create (User user, string password) {
            // validation
            if (string.IsNullOrWhiteSpace (password))
                throw new Exception ("Password is required");

            if (GoTrainDbContext.Users.Any (x => x.UserName == user.UserName))
                throw new Exception ("Username \"" + user.UserName + "\" is already taken");

            byte[] passwordHash, passwordSalt;
            PasswordHelper.CreatePasswordHash (password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            GoTrainDbContext.Users.Add (user);
            GoTrainDbContext.SaveChanges ();

            return user;
        }
        public void Delete (int id) {
            if (UserExists (id) == true) {
                var user = GoTrainDbContext.Users.Find (id);
                GoTrainDbContext.Users.Remove (user);
            }
        }

        public IEnumerable<User> GetAllUsers () {
            return GoTrainDbContext.Users;
        }

        public User GetById (int id) {
            return GoTrainDbContext.Users.Find (id);
        }

        public void Update (User userParam, string password = null) {
            var user = GoTrainDbContext.Users.Find (userParam.Id);

            if (user == null)
                throw new Exception ("User not found");

            if (userParam.UserName != user.UserName) {
                // username has changed so check if the new username is already taken
                if (GoTrainDbContext.Users.Any (x => x.UserName == userParam.UserName))
                    throw new Exception ("Username " + userParam.UserName + " is already taken");
            }

            // update user properties
            user.FirstName = userParam.FirstName;
            user.LastName = userParam.LastName;
            user.UserName = userParam.UserName;

            // update password if it was entered
            if (!string.IsNullOrWhiteSpace (password)) {
                byte[] passwordHash, passwordSalt;
                PasswordHelper.CreatePasswordHash (password, out passwordHash, out passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
            }
            GoTrainDbContext.Users.Update (user);
            GoTrainDbContext.SaveChanges ();
        }

        public bool UserExists (int userId) {
            var user = GoTrainDbContext.Users.Where (x => x.Id == userId);
            if (user == null) {
                return false;
            }
            return true;
        }
    }
}