using System;
using System.Collections.Generic;
using OnlineMarket.DataAccessLayer.Entities;
using OnlineMarket.DataAccessLayer.Interfaces;
using System.Linq;
using System.Data.Entity;

namespace OnlineMarket.DataAccessLayer.Repositories
{
    public class UserResourcesRepository : IRepository<UserResources>
    {
        private OnlineMarketContext _context;

        public UserResourcesRepository(OnlineMarketContext context)
        {
            _context = context;
        }

        public void Add(UserResources item)
        {
            _context.UserResources.Add(item);
        }

        public IList<UserResources> Find(Func<UserResources, bool> condition)
        {
            return _context.UserResources.Include("User").Include("Resource").Where(condition).ToList();
        }

        public IList<UserResources> GetAll()
        {
            return _context.UserResources.Include("User").Include("Resource").ToList();
        }

        public void Remove(UserResources item)
        {
            _context.UserResources.Remove(item);
        }

        public void Update(UserResources item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }
    }
}
