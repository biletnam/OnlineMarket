using OnlineMarket.BusinessLogicLayer.Interfaces;
using OnlineMarket.Models;
using System.Web.Http;

namespace OnlineMarket.Controllers
{
    public class OperationsController : ApiController
    {
        private IResourceService _resourceService;

        private IUserResourcesService _userResourcesService;

        private IDealService _dealService;

        public OperationsController(IResourceService resourceService, IUserResourcesService userResourcesService, IDealService dealService)
        {
            _resourceService = resourceService;
            _userResourcesService = userResourcesService;
            _dealService = dealService;
        }

        [HttpGet]
        public OperationsViewModel Get(string email)
        {
            var resourcesToBuy = _resourceService.GetResources();
            var resourcesToSell = _userResourcesService.GetUserResources(email);
            var profits = _dealService.GetProfits(email);
            return new OperationsViewModel { ResourcesToBuy = resourcesToBuy, ResourcesToSell = resourcesToSell, Profit = profits };
        }
    }
}
