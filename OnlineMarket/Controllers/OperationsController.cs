using OnlineMarket.BusinessLogicLayer.Interfaces;
using OnlineMarket.Models;
using System.Web.Http;

namespace OnlineMarket.Controllers
{
    public class OperationsController : ApiController
    {
        private IResourceService _resourceService;

        private IUserResourcesService _userResourcesService;

        public OperationsController(IResourceService resourceService, IUserResourcesService userResourcesService)
        {
            _resourceService = resourceService;
            _userResourcesService = userResourcesService;
        }

        [HttpGet]
        public OperationsViewModel Operations(string email)
        {
            var resourcesToBuy = _resourceService.GetResources();
            var resourcesToSale = _userResourcesService.GetUserResources(email);

            return new OperationsViewModel { ResourcesToBuyList = resourcesToBuy, ResourcesToSaleList = resourcesToSale };
        }

    }
}
