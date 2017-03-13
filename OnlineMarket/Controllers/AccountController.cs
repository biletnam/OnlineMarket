using OnlineMarket.BusinessLogicLayer;
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
        [Route("login")]
        [HttpPost]
        public HttpResponseMessage Login(HttpRequestMessage request, LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid) return request.CreateResponse(HttpStatusCode.BadRequest, new { success = false });

            MembershipContext membershipContext = _membershipService.ValidateUser(loginViewModel.Email, loginViewModel.Password);

            if (membershipContext.User == null) return request.CreateResponse(HttpStatusCode.OK, new { success = false });

            return request.CreateResponse(HttpStatusCode.OK, new { success = true });
        }

        [AllowAnonymous]
        [Route("register")]
        [HttpPost]
        public HttpResponseMessage Register(HttpRequestMessage request, RegistrationViewModel registrationViewModel)
        {
            try
            {
                if (!ModelState.IsValid) return request.CreateResponse(HttpStatusCode.BadRequest, new { success = false });

                var tryFindUser = _membershipService.GetUserByEmail(registrationViewModel.Email);

                return request.CreateResponse(HttpStatusCode.OK, new { success = false });   
            }
            catch
            {
                User user = _membershipService.CreateUser(registrationViewModel.Email, registrationViewModel.Password);
                return user != null ? request.CreateResponse(HttpStatusCode.OK, new { success = true }) : request.CreateResponse(HttpStatusCode.OK, new { success = false });
            }
        }

        [HttpGet]
        public bool Get(string email)
        {
            return _membershipService.IsUserAdmin(email);
        }

        [HttpPost]
        public void RefillBalance([FromBody]UpdateBalanceViewModel updateBalanceViewModel)
        {
            var user = _membershipService.GetUserByEmail(updateBalanceViewModel.Email);
            _membershipService.UpdateUserBalance(user, updateBalanceViewModel.Amount, true);
        }
    }

}
