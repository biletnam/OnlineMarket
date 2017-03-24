using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using log4net;
using Microsoft.AspNet.SignalR;
using OnlineMarket.BusinessLogicLayer.Interfaces;
using OnlineMarket.Core;
using OnlineMarket.DataAccessLayer.Entities;
using OnlineMarket.Models;

namespace OnlineMarket.Controllers
{
    [RoutePrefix("api/deal")]
    public class DealController : ApiController
    {
        private readonly IHubContext _appHub;

        private readonly IDealService _dealService;

        private readonly ILog _logger;

        private readonly IMembershipService _membershipService;

        public DealController(IDealService dealService, IMembershipService membershipService, IHubContext hubContext,
            ILog logger)
        {
            _dealService = dealService;
            _membershipService = membershipService;
            _appHub = hubContext;
            _logger = logger;
        }

        [HttpPost]
        [Route("senddeal")]
        public HttpResponseMessage SendDeal(HttpRequestMessage request, DealViewModel dealViewModel)
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

                _appHub.Clients.All.addActivity(AddRecentActivity(dealViewModel));
                return ReturnResponseForDeal(request, dealViewModel);
            }
            catch (Exception e)
            {
                _logger.Error(e);

                return request.CreateResponse(HttpStatusCode.OK,
                    new {success = false, message = Messages.CantExecuteDeal});
            }
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
                User = user,
                ResourceId = dealViewModel.ResourceId,
                Amount = dealViewModel.Price*dealViewModel.Quantity,
                Quantity = dealViewModel.Quantity
            };
        }

        private HttpResponseMessage ReturnResponseForDeal(HttpRequestMessage request, DealViewModel dealViewModel)
        {
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
    }
}