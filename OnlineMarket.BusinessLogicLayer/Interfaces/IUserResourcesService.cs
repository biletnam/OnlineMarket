using OnlineMarket.DataAccessLayer.Entities;
using System.Collections.Generic;

namespace OnlineMarket.BusinessLogicLayer.Interfaces
{
    public interface IUserResourcesService
    {
        void AddUserResources(User user);

        IList<UserResources> GetUserResources(string email);
    }
}
