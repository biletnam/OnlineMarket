using OnlineMarket.BusinessLogicLayer.Interfaces;
using OnlineMarket.DataAccessLayer.Entities;
using OnlineMarket.Models;
using System.Web.Http;

namespace OnlineMarket.Controllers
{
    public class DealController : ApiController
    {
        private IDealService _dealService;

        private IUserResourcesService _userResourcesService;

        public DealController(IDealService dealService, IUserResourcesService userResourcesService)
        {
            _dealService = dealService;
            _userResourcesService = userResourcesService;
        }

        [HttpPost]
        public void AddDeal(DealViewModel dealViewModel)
        {
            if (dealViewModel.IsPurchase)
            {
                _dealService.AddPurchaseDeal(new Deal { UserId = dealViewModel.UserId, ResourceId = dealViewModel.ResourceId, Amount = dealViewModel.Amount, Quantity = dealViewModel.Quantity });
                //update user resources
            }
            {
                _dealService.AddSaleDeal(new Deal { UserId = dealViewModel.UserId, ResourceId = dealViewModel.ResourceId, Amount = dealViewModel.Amount, Quantity = dealViewModel.Quantity });
                //update user resources
            }
        }
    }
}
