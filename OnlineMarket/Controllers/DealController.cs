﻿using OnlineMarket.BusinessLogicLayer.Interfaces;
using OnlineMarket.DataAccessLayer.Entities;
using OnlineMarket.Models;
using System.Web.Http;
using System.Net.Http;
using System.Net;
using OnlineMarket.Hubs;
using Microsoft.AspNet.SignalR;

namespace OnlineMarket.Controllers
{
    public class DealController : ApiController
    {
        private IDealService _dealService;

        private IUserResourcesService _userResourcesService;

        private IMembershipService _membershipService;

        private IHubContext _appHub;

        public DealController(IDealService dealService, IUserResourcesService userResourcesService, IMembershipService membershipService, IHubContext hubContext)
        {
            _dealService = dealService;
            _userResourcesService = userResourcesService;
            _membershipService = membershipService;
            _appHub = hubContext;
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

                UpdateUserInformation(user, dealViewModel);

                return request.CreateResponse(HttpStatusCode.OK, new { success = true, amount = dealViewModel.Quantity * dealViewModel.Price, add = !dealViewModel.IsPurchase, id = dealViewModel.ResourceId, quantity = dealViewModel.Quantity });
            }
            catch
            {
                return request.CreateResponse(HttpStatusCode.OK, new { success = false, message = "Can't execute deal." });
            }
        }

        private void UpdateUserInformation(User user, DealViewModel dealViewModel)
        {
            _userResourcesService.UpdateUserResources(new UserResources { UserId = user.Id, ResourceId = dealViewModel.ResourceId, Quantity = dealViewModel.Quantity }, dealViewModel.IsPurchase);
            _membershipService.UpdateUserBalance(user, dealViewModel.Price * dealViewModel.Quantity, !dealViewModel.IsPurchase);
            _appHub.Clients.All.addActivity(AddRecentActivity(dealViewModel));
        }

        private string AddRecentActivity(DealViewModel dealViewModel)
        {
            var dealType = dealViewModel.IsPurchase ? "bought" : "sold";
            return $"{dealViewModel.Quantity} items of {dealViewModel.ResourceTitle} were {dealType}.";
        }

        
    }
}
