using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using OnlineMarket.DataAccessLayer.Entities;
using OnlineMarket.DataAccessLayer.Interfaces;

namespace OnlineMarket.DataAccessLayer.Repositories
{
    public class ResourceRepository : IRepository<Resource>
    {
        private readonly OnlineMarketContext _context;

        public ResourceRepository(OnlineMarketContext context)
        {
            _context = context;
        }

        public void Add(Resource item)
        {
            _context.Resources.Add(item);
        }

        public IList<Resource> Find(Func<Resource, bool> condition)
        {
            return _context.Resources.Where(condition).ToList();
        }

        public IList<Resource> GetAll()
        {
            return _context.Resources.ToList();
        }

        public void Remove(Resource item)
        {
            _context.Resources.Remove(item);
        }

        public void Update(Resource item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }
    }
}