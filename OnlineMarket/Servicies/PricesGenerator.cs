using System;
using System.Threading;
using Microsoft.AspNet.SignalR;
using OnlineMarket.BusinessLogicLayer.Interfaces;
using OnlineMarket.Interfaces;

namespace OnlineMarket.Servicies
{
    public class PricesGenerator : IPricesGenerator
    {
        private static IPricesGenerator _instance;

        private readonly IHubContext _appHub;

        private const double MinValue = 10.0;

        private const double MaxValue = 50.0;

        private const int TimeInterval = 100000;

        public double[] CurrentPrices { get; set; }

        private PricesGenerator(IResourceService resourceService, IHubContext hubContext)
        {
            _appHub = hubContext;
            var count = resourceService.GetResources().Count;
            CurrentPrices = new double[count];
            ThreadPool.QueueUserWorkItem(Generate, count);
        }

        public static IPricesGenerator GetInstance(IResourceService resourceService, IHubContext hubContext)
        {
            return _instance ?? (_instance = new PricesGenerator(resourceService, hubContext));
        }

        private double[] GetNewPrices(int count, Random random)
        {
            var prices = new double[count];

            for (var i = 0; i < count; i++)
            {
                prices[i] = random.NextDouble()*(MaxValue - MinValue) + MinValue;
            }

            CurrentPrices = prices;
            return prices;
        }

        public void Generate(object count)
        {
            var random = new Random();
            while (true)
            {
                _appHub.Clients.All.addNewPrices(GetNewPrices((int) count, random));
                Thread.Sleep(TimeInterval);
            }
        }
    }
}