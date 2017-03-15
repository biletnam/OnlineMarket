using AutoMapper;
using Microsoft.AspNet.SignalR;
using OnlineMarket.BusinessLogicLayer;
using OnlineMarket.BusinessLogicLayer.Interfaces;
using OnlineMarket.DataAccessLayer.Entities;
using OnlineMarket.Hubs;
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

        private IHubContext _appHub;

        public AccountController(IMembershipService membershipService, IHubContext hubContext)
        {
            _membershipService = membershipService;
            _appHub = hubContext;
        }

        [Route("login")]
        [HttpPost]
        public HttpResponseMessage Login(HttpRequestMessage request, LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid) return request.CreateResponse(HttpStatusCode.BadRequest, new { success = false, message = "Input all fields." });

            MembershipContext membershipContext = _membershipService.ValidateUser(loginViewModel.Email, loginViewModel.Password);

            if (membershipContext.User == null) return request.CreateResponse(HttpStatusCode.OK, new { success = false, message = "Check email and password" });

            

            return request.CreateResponse(HttpStatusCode.OK, new { success = true });
        }

        [Route("register")]
        [HttpPost]
        public HttpResponseMessage Register(HttpRequestMessage request, RegistrationViewModel registrationViewModel)
        {
            try
            {
                if (!ModelState.IsValid) return request.CreateResponse(HttpStatusCode.BadRequest, new { success = false, message = "Input all fields." });

                var tryFindUser = _membershipService.GetUserByEmail(registrationViewModel.Email);

                return request.CreateResponse(HttpStatusCode.OK, new { success = false, message = "You can't use this login." });   
            }
            catch
            {
                User user = _membershipService.CreateUser(registrationViewModel.Email, registrationViewModel.Password);

                _appHub.Clients.All.addUser(Mapper.Map<UserViewModel>(user));

                return user != null ? request.CreateResponse(HttpStatusCode.OK, new { success = true }) : request.CreateResponse(HttpStatusCode.OK, new { success = false, message="Can't create user." });
            }
        }

        [HttpGet]
        public HttpResponseMessage Get(HttpRequestMessage request, string email)
        {
            try
            {
                return request.CreateResponse(HttpStatusCode.OK, new { success = true, isAdmin = _membershipService.IsUserAdmin(email)});
            }
            catch
            {
                return request.CreateResponse(HttpStatusCode.OK, new { success = false, message = "Can't check user's rights." });
            }
        }

        [Route("refillbalance")]
        [HttpPost]
        public HttpResponseMessage RefillBalance(HttpRequestMessage request, [FromBody]UpdateBalanceViewModel updateBalanceViewModel)
        {
            try
            {
                var user = _membershipService.GetUserByEmail(updateBalanceViewModel.Email);
                _membershipService.UpdateUserBalance(user, updateBalanceViewModel.Amount, true);
                return request.CreateResponse(HttpStatusCode.OK, new { success = true, amount = updateBalanceViewModel.Amount, add = true });
            }
            catch
            {
                return request.CreateResponse(HttpStatusCode.OK, new { success = false, message = "Can't refill balance" });
            }
        }
    }

}
