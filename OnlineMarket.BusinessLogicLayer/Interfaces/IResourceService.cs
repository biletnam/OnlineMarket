using OnlineMarket.DataAccessLayer.Entities;
using System.Collections.Generic;

namespace OnlineMarket.BusinessLogicLayer.Interfaces
{
    public interface IResourceService
    {
        IList<UserResources> GetResources();

        void ModifyResourcePrice(UserResources resource, double price);
    }
}
