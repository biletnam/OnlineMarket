using OnlineMarket.BusinessLogicLayer.Interfaces;
using OnlineMarket.DataAccessLayer.Entities;
using OnlineMarket.Models;
using System.Web.Http;
using System.Net.Http;
using System.Net;

namespace OnlineMarket.Controllers
{
    public class DealController : ApiController
    {
        private IDealService _dealService;

        private IUserResourcesService _userResourcesService;

        private IMembershipService _membershipService;

        public DealController(IDealService dealService, IUserResourcesService userResourcesService, IMembershipService membershipService)
        {
            _dealService = dealService;
            _userResourcesService = userResourcesService;
            _membershipService = membershipService;
        }

        [HttpPost]
        public HttpResponseMessage Post(HttpRequestMessage request, [FromBody]DealViewModel dealViewModel)
        {
            try
            {
                var user = _membershipService.GetUserByEmail(dealViewModel.Email);
                if (dealViewModel.IsPurchase)
                {
                    _dealService.AddPurchaseDeal(new Deal { UserId = user.Id, ResourceId = dealViewModel.ResourceId, Amount = dealViewModel.Price * dealViewModel.Quantity, Quantity = dealViewModel.Quantity });
                }
                else
                {
                    _dealService.AddSaleDeal(new Deal { UserId = user.Id, ResourceId = dealViewModel.ResourceId, Amount = dealViewModel.Price * dealViewModel.Quantity, Quantity = dealViewModel.Quantity });
                }

                _userResourcesService.UpdateUserResources(new UserResources { UserId = user.Id, ResourceId = dealViewModel.ResourceId, Quantity = dealViewModel.Quantity }, dealViewModel.IsPurchase);
                _membershipService.UpdateUserBalance(user, dealViewModel.Price * dealViewModel.Quantity, !dealViewModel.IsPurchase);

                return request.CreateResponse(HttpStatusCode.OK, new { success = true, amount = dealViewModel.Quantity * dealViewModel.Price, add = !dealViewModel.IsPurchase, id = dealViewModel.ResourceId, quantity = dealViewModel.Quantity });
            }
            catch
            {
                return request.CreateResponse(HttpStatusCode.OK, new { success = false, message = "Can't execute deal." });
            }
        }
    }
}
