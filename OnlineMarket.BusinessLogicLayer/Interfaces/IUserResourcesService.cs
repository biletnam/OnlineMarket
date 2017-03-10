using OnlineMarket.DataAccessLayer.Entities;
using System.Collections.Generic;

namespace OnlineMarket.BusinessLogicLayer.Interfaces
{
    public interface IUserResourcesService
    {
        void UpdateUserResources(UserResources item, bool isPurchase);

        IList<UserResources> GetUserResources(string email);
    }
}
