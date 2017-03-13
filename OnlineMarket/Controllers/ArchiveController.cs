using AutoMapper;
using OnlineMarket.BusinessLogicLayer.Interfaces;
using OnlineMarket.Models;
using System.Collections.Generic;
using System.Text;
using System.Web.Http;

namespace OnlineMarket.Controllers
{
    public class ArchiveController : ApiController
    {
        private IDealService _dealService;

        private const int _recentActivitiesCount = 5;

        public ArchiveController(IDealService dealService)
        {
            _dealService = dealService;
        }

        //[Authorize]
        [HttpGet]
        public IList<ArchiveViewModel> GetArchive(string email)
        {
            var deals = _dealService.GetDealsByUser(email);
            return Mapper.Map<IList<ArchiveViewModel>>(deals);
        }

        [HttpGet]
        public IList<string> GetRecentActivities()
        {
            var activities = _dealService.GetActivities(_recentActivitiesCount);
            var activitiesToString = new List<string>();

            for(var i = activities.Count - 1; i >= 0; i--)
            {
                var dealType = activities[i].DealType.Type == "Purchase" ? "bought" : "sold";
                activitiesToString.Add($"{activities[i].Quantity} items of {activities[i].Resource.Title} were {dealType}.");
            }

            return activitiesToString;
        }
    }
}
