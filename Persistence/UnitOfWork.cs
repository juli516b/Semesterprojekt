using Microsoft.EntityFrameworkCore;
using Semesterprojekt.Core;
using Semesterprojekt.Core.Repositories;
using Semesterprojekt.Persistence;
using Semesterprojekt.Persistence.Repositories;

namespace Queries.Persistence {
    public class UnitOfWork : IUnitOfWork {
        private readonly GoTrainDbContext _context;

        public UnitOfWork (GoTrainDbContext context) 
        {
            _context = context;
            Users = new UserRepository(_context);
        }

        public IUserRepository Users { get; private set; }

        public int Complete () {
            return _context.SaveChanges ();
        }

        public void Dispose () {
            _context.Dispose ();
        }
    }
}