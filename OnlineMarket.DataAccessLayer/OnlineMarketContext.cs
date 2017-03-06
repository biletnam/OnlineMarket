using OnlineMarket.DataAccessLayer.Entities;
using System.Data.Entity;

namespace OnlineMarket.DataAccessLayer
{
    public class OnlineMarketContext : DbContext
    {
        public OnlineMarketContext() : base("OrderingFoodDB")
        { }

        public DbSet<User> Users { get; set; }

        public DbSet<Deal> Deals { get; set; }

        public DbSet<Resource> Resources { get; set; }
    }
}
