using OnlineMarket.BusinessLogicLayer.Interfaces;
using OnlineMarket.DataAccessLayer.Entities;
using OnlineMarket.Models;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OnlineMarket.Controllers
{
    [RoutePrefix("api/Account")]
    public class AccountController : ApiController
    {
        private IMembershipService _membershipService;

        public AccountController(IMembershipService membershipService)
        {
            _membershipService = membershipService;
        }

        [AllowAnonymous]
        [Route("register")]
        [HttpPost]
        public HttpResponseMessage Register(HttpRequestMessage request, RegistrationViewModel registrationViewModel)
        {
            if (!ModelState.IsValid) return request.CreateResponse(HttpStatusCode.BadRequest, new { success = false });

            User user = _membershipService.CreateUser(registrationViewModel.Email, registrationViewModel.Password);

            return user != null ? request.CreateResponse(HttpStatusCode.OK, new { success = true }) : request.CreateResponse(HttpStatusCode.OK, new { success = false });
        }
    }

}
