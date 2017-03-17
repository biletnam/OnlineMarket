using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using log4net;
using Microsoft.AspNet.SignalR;
using OnlineMarket.BusinessLogicLayer.Interfaces;
using OnlineMarket.DataAccessLayer.Entities;
using OnlineMarket.Models;

namespace OnlineMarket.Controllers
{
    public class DealController : ApiController
    {
        private readonly IDealService _dealService;

        private readonly IUserResourcesService _userResourcesService;

        private readonly IMembershipService _membershipService;

        private readonly IHubContext _appHub;

        private readonly ILog _logger;

        public DealController(IDealService dealService, IUserResourcesService userResourcesService,
            IMembershipService membershipService, IHubContext hubContext, ILog logger)
        {
            _dealService = dealService;
            _userResourcesService = userResourcesService;
            _membershipService = membershipService;
            _appHub = hubContext;
            _logger = logger;
        }

        [HttpPost]
        public HttpResponseMessage Post(HttpRequestMessage request, [FromBody] DealViewModel dealViewModel)
        {
            try
            {
                var user = _membershipService.GetUserByEmail(dealViewModel.Email);
                if (dealViewModel.IsPurchase)
                {
                    _dealService.AddPurchaseDeal(DealFromViewModel(user, dealViewModel));
                }
                else
                {
                    _dealService.AddSaleDeal(DealFromViewModel(user, dealViewModel));
                }

                UpdateUserInformation(user, dealViewModel);

                return request.CreateResponse(HttpStatusCode.OK,
                    new
                    {
                        success = true,
                        amount = dealViewModel.Quantity*dealViewModel.Price,
                        add = !dealViewModel.IsPurchase,
                        id = dealViewModel.ResourceId,
                        quantity = dealViewModel.Quantity
                    });
            }
            catch (Exception e)
            {
                _logger.Error(e);

                return request.CreateResponse(HttpStatusCode.OK, new {success = false, message = "Can't execute deal."});
            }
        }

        private void UpdateUserInformation(User user, DealViewModel dealViewModel)
        {
            _userResourcesService.UpdateUserResources(UserResourcesFromViewModel(user, dealViewModel), dealViewModel.IsPurchase);
            _membershipService.UpdateUserBalance(user, dealViewModel.Price*dealViewModel.Quantity,!dealViewModel.IsPurchase);
            _appHub.Clients.All.addActivity(AddRecentActivity(dealViewModel));
        }

        private string AddRecentActivity(DealViewModel dealViewModel)
        {
            var dealType = dealViewModel.IsPurchase ? "bought" : "sold";

            return $"{dealViewModel.Quantity} items of {dealViewModel.ResourceTitle} were {dealType}.";
        }

        private Deal DealFromViewModel(User user, DealViewModel dealViewModel)
        {
            return new Deal
            {
                UserId = user.Id,
                ResourceId = dealViewModel.ResourceId,
                Amount = dealViewModel.Price*dealViewModel.Quantity,
                Quantity = dealViewModel.Quantity
            };
        }

        private UserResources UserResourcesFromViewModel(User user, DealViewModel dealViewModel)
        {
            return new UserResources
            {
                UserId = user.Id,
                ResourceId = dealViewModel.ResourceId,
                Quantity = dealViewModel.Quantity
            };
        }
    }
}
