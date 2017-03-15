using Microsoft.AspNet.SignalR;
using OnlineMarket.BusinessLogicLayer.Interfaces;
using OnlineMarket.Hubs;
using OnlineMarket.Interfaces;
using System;
using System.Threading;

namespace OnlineMarket.Servicies
{
    public class PricesGenerator : IPricesGenerator
    {
        private IResourceService _resourceService;

        private IHubContext _appHub;

        private const double _minValue = 10.0;

        private const double _maxValue = 50.0;

        public double[] CurrentPrices { get; set; }

        public PricesGenerator(IResourceService resourceService, IHubContext hubContext)
        {
            _resourceService = resourceService;
            _appHub = hubContext;
            var count = _resourceService.GetResources().Count;
            CurrentPrices = new double[count];
            ThreadPool.QueueUserWorkItem(Generate,count);
        }

        private double[] GetNewPrices(int count, Random random)
        {
            var prices = new double[count];

            for(var i =0; i< count; i++)
            {
                prices[i] = random.NextDouble()*(_maxValue - _minValue) + _minValue;
            }

            CurrentPrices = prices;
            return prices;
        }

        public void Generate(object count)
        {
            var random = new Random();
            while(true)
            {
                _appHub.Clients.All.addNewPrices(GetNewPrices((int)count,random));
                Thread.Sleep(100000);
            }
        }
    }
}