using OnlineMarket.DataAccessLayer.Entities;
using OnlineMarket.DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace OnlineMarket.DataAccessLayer.Repositories
{
    public class DealRepository : IRepository<Deal>
    {
        private OnlineMarketContext _context;

        public DealRepository(OnlineMarketContext context)
        {
            _context = context;
        }

        public void Add(Deal item)
        {
            item.Date = DateTime.Today;
            _context.Deals.Add(item);
        }

        public IList<Deal> Find(Func<Deal, bool> condition)
        {
            return _context.Deals.Include("User").Include("Resource").Include("DealType").Where(condition).ToList();
        }

        public IList<Deal> GetAll()
        {
            return _context.Deals.Include("User").Include("Resource").Include("DealType").ToList();
        }

        public void Remove(Deal item)
        {
            _context.Deals.Remove(item);
        }

        public void Update(Deal item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }
    }
}
