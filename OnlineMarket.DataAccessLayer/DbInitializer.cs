using System.Data.Entity;
using OnlineMarket.DataAccessLayer.Entities;

namespace OnlineMarket.DataAccessLayer
{
    public class DbInitializer : CreateDatabaseIfNotExists<OnlineMarketContext>
    {
        protected override void Seed(OnlineMarketContext context)
        {
            context.Roles.Add(new Role {Title = "Administrator"});
            context.Roles.Add(new Role {Title = "User"});
            context.Roles.Add(new Role {Title = "Banned user"});

            context.DealTypes.Add(new DealType {Type = "Purchase"});
            context.DealTypes.Add(new DealType {Type = "Sale"});

            context.Resources.Add(new Resource {Title = "Wood", Price = 13.40});
            context.Resources.Add(new Resource {Title = "Iron", Price = 30.50});
            context.Resources.Add(new Resource {Title = "Oil", Price = 28.00});

            context.SaveChanges();
        }
    }
}