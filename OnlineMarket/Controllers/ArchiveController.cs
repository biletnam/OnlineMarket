using AutoMapper;
using OnlineMarket.BusinessLogicLayer.Interfaces;
using OnlineMarket.Models;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
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

        [HttpGet]
        public HttpResponseMessage GetArchive(HttpRequestMessage request, string email)
        {
            try
            {
                var deals = _dealService.GetDealsByUser(email);
                return request.CreateResponse(HttpStatusCode.OK, new { success = true, archive = Mapper.Map<IList<ArchiveViewModel>>(deals)}); 
            }
            catch
            {
                return request.CreateResponse(HttpStatusCode.OK, new { success = false, message = "Can't load archive." });
            }
        }

        [HttpGet]
        public HttpResponseMessage GetRecentActivities(HttpRequestMessage request)
        {
            try
            {
                var activities = _dealService.GetActivities(_recentActivitiesCount);
                var activitiesToString = new List<string>();

                for (var i = activities.Count - 1; i >= 0; i--)
                {
                    var dealType = activities[i].DealType.Type == "Purchase" ? "bought" : "sold";
                    activitiesToString.Add($"{activities[i].Quantity} items of {activities[i].Resource.Title} were {dealType}.");
                }

                return request.CreateResponse(HttpStatusCode.OK, new { success = true, activities = activitiesToString });
            }
            catch
            {
                return request.CreateResponse(HttpStatusCode.OK, new { success = false, message = "Can't load recent activities." });
            }
        }
    }
}
