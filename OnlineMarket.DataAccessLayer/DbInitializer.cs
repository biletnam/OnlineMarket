using OnlineMarket.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineMarket.DataAccessLayer
{
    public class DbInitializer : CreateDatabaseIfNotExists<OnlineMarketContext>
    {
        protected override void Seed(OnlineMarketContext context)
        {
            context.Roles.Add(new Role { Title = "Administrator" });
            context.Roles.Add(new Role { Title = "User" });
            context.Roles.Add(new Role { Title = "Banned user" });

            context.DealTypes.Add(new DealType { Type = "Purchase" });
            context.DealTypes.Add(new DealType { Type = "Sale" });

            context.Resources.Add(new UserResources { Title = "Wood", Price = 13.40 });
            context.Resources.Add(new UserResources { Title = "Iron", Price = 30.50 });
            context.Resources.Add(new UserResources { Title = "Oil", Price = 28.00 });

            context.SaveChanges();
        }
    }
}
