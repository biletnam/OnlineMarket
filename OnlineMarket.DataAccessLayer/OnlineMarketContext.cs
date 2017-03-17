using System.Data.Entity;
using OnlineMarket.DataAccessLayer.Entities;

namespace OnlineMarket.DataAccessLayer
{
    public class OnlineMarketContext : DbContext
    {
        static OnlineMarketContext()
        {
            Database.SetInitializer(new DbInitializer());
        }

        public OnlineMarketContext() : base("OnlineMarketDB")
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Deal> Deals { get; set; }

        public DbSet<Resource> Resources { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<DealType> DealTypes { get; set; }

        public DbSet<UserResources> UserResources { get; set; }
    }
}