﻿using OnlineMarket.DataAccessLayer.Entities;
using OnlineMarket.DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace OnlineMarket.DataAccessLayer.Repositories
{
    public class ResourceRepository : IRepository<UserResources>
    {
        private OnlineMarketContext _context;

        public ResourceRepository(OnlineMarketContext context)
        {
            _context = context;
        }

        public void Add(UserResources item)
        {
            _context.Resources.Add(item);
        }

        public IList<UserResources> Find(Func<UserResources, bool> condition)
        {
            return _context.Resources.Where(condition).ToList();
        }

        public IList<UserResources> GetAll()
        {
            return _context.Resources.ToList();
        }

        public void Remove(UserResources item)
        {
            _context.Resources.Remove(item);
        }

        public void Update(UserResources item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }
    }
}
