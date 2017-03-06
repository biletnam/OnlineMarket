using OnlineMarket.DataAccessLayer.Entities;

namespace OnlineMarket.DataAccessLayer.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<Deal> DealRepository { get; }

        IRepository<Resource> ResourceRepository { get; }

        IRepository<User> UserRepository { get; }

        void SaveChanges();
    }
}
