using System;
using OnlineMarket.DataAccessLayer.Entities;
using OnlineMarket.DataAccessLayer.Interfaces;

namespace OnlineMarket.DataAccessLayer
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly OnlineMarketContext _context;

        private readonly IRepository<Deal> _dealRepository;

        private readonly IRepository<Resource> _resourceRepository;

        private readonly IRepository<User> _userRepository;

        private readonly IRepository<UserResources> _userResourcesRepository;

        private bool _disposed;

        public UnitOfWork(OnlineMarketContext context, IRepository<Deal> dealRepository,
            IRepository<Resource> resourceRepository, IRepository<User> userRepository,
            IRepository<UserResources> userResourcesRepository)
        {
            _context = context;
            _dealRepository = dealRepository;
            _resourceRepository = resourceRepository;
            _userRepository = userRepository;
            _userResourcesRepository = userResourcesRepository;
        }

        public IRepository<Deal> DealRepository
        {
            get
            {
                if (_disposed) throw new ObjectDisposedException(nameof(UnitOfWork));

                return _dealRepository;
            }
        }

        public IRepository<Resource> ResourceRepository
        {
            get
            {
                if (_disposed) throw new ObjectDisposedException(nameof(UnitOfWork));

                return _resourceRepository;
            }
        }

        public IRepository<User> UserRepository
        {
            get
            {
                if (_disposed) throw new ObjectDisposedException(nameof(UnitOfWork));

                return _userRepository;
            }
        }

        public IRepository<UserResources> UserResourcesRepository
        {
            get
            {
                if (_disposed) throw new ObjectDisposedException(nameof(UnitOfWork));

                return _userResourcesRepository;
            }
        }

        public void SaveChanges()
        {
            if (_disposed) throw new ObjectDisposedException(nameof(UnitOfWork));

            _context.SaveChanges();
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing)
            {
                _context?.Dispose();
                _disposed = true;
            }
        }
    }
}