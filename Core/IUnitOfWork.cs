using System;
using Semesterprojekt.Core.Repositories;

namespace Semesterprojekt.Core {
    public interface IUnitOfWork : IDisposable 
    {
        IUserRepository Users { get; }
        int Complete ();
    }
}