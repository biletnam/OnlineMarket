using AutoMapper;
using OnlineMarket.BusinessLogicLayer.Interfaces;
using OnlineMarket.Models;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OnlineMarket.Controllers
{
    public class UserManagerController : ApiController
    {
        private IMembershipService _membershipService;

        public UserManagerController(IMembershipService membershipService)
        {
            _membershipService = membershipService;
        }

        [HttpGet]
        public HttpResponseMessage Get(HttpRequestMessage request)
        {
            try
            {
                var users = Mapper.Map<IList<UserViewModel>>(_membershipService.GetUsers());
                return request.CreateResponse(HttpStatusCode.OK, new { success = true, users = users });
            }
            catch
            {
                return request.CreateResponse(HttpStatusCode.OK, new { success = false, message = "Can't load users." });
            }
        }

        [HttpPost]
        public HttpResponseMessage Post(HttpRequestMessage request, [FromBody]UserViewModel userViewModel)
        {
            try
            {
                _membershipService.ChangeUserRole(userViewModel.Id, userViewModel.RoleId);
                return request.CreateResponse(HttpStatusCode.OK, new { success = true }); 
            }
            catch
            {
                return request.CreateResponse(HttpStatusCode.OK, new { success = false, message = "Can't change role." });
            }
        }
    }
}
