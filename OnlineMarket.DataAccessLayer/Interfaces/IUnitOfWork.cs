using OnlineMarket.DataAccessLayer.Entities;
using System;

namespace OnlineMarket.DataAccessLayer.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Deal> DealRepository { get; }

        IRepository<UserResources> ResourceRepository { get; }

        IRepository<User> UserRepository { get; }

        IRepository<UserResources> UserResourcesRepository { get; }

        void SaveChanges();
    }
}
