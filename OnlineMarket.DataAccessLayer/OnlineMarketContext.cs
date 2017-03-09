using OnlineMarket.DataAccessLayer.Entities;
using System.Data.Entity;

namespace OnlineMarket.DataAccessLayer
{
    public class OnlineMarketContext : DbContext
    {
        public OnlineMarketContext() : base("OnlineMarketDB")
        { }

        static OnlineMarketContext()
        {
            Database.SetInitializer(new DbInitializer());
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Deal> Deals { get; set; }

        public DbSet<UserResources> Resources { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<DealType> DealTypes { get; set; }

        public DbSet<UserResources> UserResources { get; set; }
    }
}
