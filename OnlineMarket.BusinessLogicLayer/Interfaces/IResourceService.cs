using OnlineMarket.DataAccessLayer.Entities;
using System.Collections.Generic;

namespace OnlineMarket.BusinessLogicLayer.Interfaces
{
    public interface IResourceService
    {
        IList<Resource> GetResources();

        void ModifyResourcePrice(Resource resource, double price);
    }
}
