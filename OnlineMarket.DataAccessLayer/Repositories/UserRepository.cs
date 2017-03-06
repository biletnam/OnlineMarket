using OnlineMarket.DataAccessLayer.Entities;
using OnlineMarket.DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace OnlineMarket.DataAccessLayer.Repositories
{
    public class UserRepository : IRepository<User>
    {
        private OnlineMarketContext _context;

        public UserRepository(OnlineMarketContext context)
        {
            _context = context;
        }

        public IList<User> GetAll()
        {
            return _context.Users.Include("Role").ToList();
        }

        public IList<User> Find(Func<User, bool> condition)
        {
            return _context.Users.Include("Role").Where(condition).ToList();
        }

        public void Add(User item)
        {
            _context.Users.Add(item);
        }

        public void Update(User item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }

        public void Remove(User item)
        {
            _context.Users.Remove(item);
        }
    }
}
