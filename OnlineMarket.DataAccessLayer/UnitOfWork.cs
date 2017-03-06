using OnlineMarket.DataAccessLayer.Entities;
using OnlineMarket.DataAccessLayer.Interfaces;
using System;

namespace OnlineMarket.DataAccessLayer
{
    public class UnitOfWork : IUnitOfWork
    {
        private OnlineMarketContext _context;

        private IRepository<Deal> _dealRepository;

        private IRepository<Resource> _resourceRepository;

        private IRepository<User> _userRepository;

        private bool _disposed = false;

        public UnitOfWork(OnlineMarketContext context, IRepository<Deal> dealRepository, IRepository<Resource> resourceRepository, IRepository<User> userRepository)
        {
            _context = context;
            _dealRepository = dealRepository;
            _resourceRepository = resourceRepository;
            _userRepository = userRepository;
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

        public void SaveChanges()
        {
            if (_disposed) throw new ObjectDisposedException(nameof(UnitOfWork));

            _context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing)
            {
                if (_context != null)
                    _context.Dispose();

                _disposed = true;
            }
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }
        }
    }
}
