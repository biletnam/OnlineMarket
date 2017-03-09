using OnlineMarket.DataAccessLayer.Entities;
using System.Collections.Generic;

namespace OnlineMarket.BusinessLogicLayer.Interfaces
{
    public interface IUserResourcesService
    {
        //void AddResourceToUser();

        IList<UserResources> GetUserResources(string email);

        //void UpdateResourceAmount();


    }
}
