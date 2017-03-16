﻿using log4net;
using Microsoft.AspNet.SignalR;
using OnlineMarket.BusinessLogicLayer.Interfaces;
using OnlineMarket.Interfaces;
using OnlineMarket.Models;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OnlineMarket.Controllers
{
    [RoutePrefix("api/Operations")]
    public class OperationsController : ApiController
    {
        private IResourceService _resourceService;

        private IUserResourcesService _userResourcesService;

        private IDealService _dealService;

        private IPricesGenerator _pricesGenerator;

        private IHubContext _appHub;

        private ILog _logger;

        public OperationsController(IResourceService resourceService, IUserResourcesService userResourcesService, IDealService dealService, IPricesGenerator pricesGenerator, IHubContext hubContext, ILog logger)
        {
            _resourceService = resourceService;
            _userResourcesService = userResourcesService;
            _dealService = dealService;
            _pricesGenerator = pricesGenerator;
            _appHub = hubContext;
            _logger = logger;
        }

        [HttpGet]
        public HttpResponseMessage Get(HttpRequestMessage request, string email)
        {
            try
            {
                var resourcesToBuy = _resourceService.GetResources();
                var resourcesToSell = _userResourcesService.GetUserResources(email);
                var profits = _dealService.GetProfits(email);
                var operationViewModel = new OperationsViewModel { ResourcesToBuy = resourcesToBuy, ResourcesToSell = resourcesToSell, Profit = profits, Balance = resourcesToSell.First().User.Balance };

                return request.CreateResponse(HttpStatusCode.OK, new { success = true, operations = operationViewModel });
            }
            catch(Exception e)
            {
                _logger.Error(e);
                return request.CreateResponse(HttpStatusCode.OK, new { success = false, message = "Can't get resources and profits." });
            }
        }

        [HttpGet]
        public HttpResponseMessage Get(HttpRequestMessage request)
        {
            try
            {
                return request.CreateResponse(HttpStatusCode.OK, new { success = true, resources = _resourceService.GetResources() });
            }
            catch(Exception e)
            {
                _logger.Error(e);
                return request.CreateResponse(HttpStatusCode.OK, new { success = false, message = "Can't get resources" });
            }
        }

        [HttpGet]
        [Route("sendcurrentprices")]
        public HttpResponseMessage SendCurrentPrices(HttpRequestMessage request)
        {
            try
            {
                _appHub.Clients.All.addNewPrices(_pricesGenerator.CurrentPrices);
                return request.CreateResponse(HttpStatusCode.OK, new { success = true });
            }
            catch(Exception e)
            {
                _logger.Error(e);
                return request.CreateResponse(HttpStatusCode.OK, new { success = false, message = "Can't send new prices" });
            }
        }
    }
}
