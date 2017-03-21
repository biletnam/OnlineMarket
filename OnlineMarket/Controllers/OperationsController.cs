using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using log4net;
using Microsoft.AspNet.SignalR;
using OnlineMarket.BusinessLogicLayer.Interfaces;
using OnlineMarket.Core;
using OnlineMarket.Interfaces;
using OnlineMarket.Models;
using OnlineMarket.Servicies;

namespace OnlineMarket.Controllers
{
    [RoutePrefix("api/Operations")]
    public class OperationsController : ApiController
    {
        private readonly IResourceService _resourceService;

        private readonly IUserResourcesService _userResourcesService;

        private readonly IDealService _dealService;

        private readonly IPricesGenerator _pricesGenerator;

        private readonly IHubContext _appHub;

        private readonly ILog _logger;

        public OperationsController(IResourceService resourceService, IUserResourcesService userResourcesService,
            IDealService dealService, IHubContext hubContext, ILog logger)
        {
            _resourceService = resourceService;
            _userResourcesService = userResourcesService;
            _dealService = dealService;
            _appHub = hubContext;
            _logger = logger;
            _pricesGenerator = PricesGenerator.GetInstance(_resourceService, _appHub);
        }

        [HttpGet]
        public HttpResponseMessage Get(HttpRequestMessage request, string email)
        {
            try
            {
                var resourcesToBuy = _resourceService.GetResources();
                var resourcesToSell = _userResourcesService.GetUserResources(email);
                var profits = _dealService.GetProfits(email);
                var operationViewModel = new OperationsViewModel
                {
                    ResourcesToBuy = resourcesToBuy,
                    ResourcesToSell = resourcesToSell,
                    Profit = profits,
                    Balance = resourcesToSell.First().User.Balance
                };

                return request.CreateResponse(HttpStatusCode.OK, new {success = true, operations = operationViewModel});
            }
            catch (Exception e)
            {
                _logger.Error(e);

                return request.CreateResponse(HttpStatusCode.OK,
                    new {success = false, message = Messages.CantLoadResources });
            }
        }

        [HttpGet]
        public HttpResponseMessage Get(HttpRequestMessage request)
        {
            try
            {
                return request.CreateResponse(HttpStatusCode.OK,
                    new {success = true, resources = _resourceService.GetResources()});
            }
            catch (Exception e)
            {
                _logger.Error(e);

                return request.CreateResponse(HttpStatusCode.OK, new {success = false, message = Messages.CantLoadResources });
            }
        }

        [HttpGet]
        [Route("sendcurrentprices")]
        public HttpResponseMessage SendCurrentPrices(HttpRequestMessage request)
        {
            try
            {
                _appHub.Clients.All.addNewPrices(_pricesGenerator.CurrentPrices);
                return request.CreateResponse(HttpStatusCode.OK, new {success = true});
            }
            catch (Exception e)
            {
                _logger.Error(e);
                return request.CreateResponse(HttpStatusCode.OK,
                    new {success = false, message = Messages.CantSendPrices });
            }
        }
    }
}
