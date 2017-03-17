using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using log4net;
using Microsoft.AspNet.SignalR;
using OnlineMarket.BusinessLogicLayer.Interfaces;
using OnlineMarket.Models;

namespace OnlineMarket.Controllers
{
    [RoutePrefix("api/Account")]
    public class AccountController : ApiController
    {
        private readonly IMembershipService _membershipService;

        private readonly IHubContext _appHub;

        private readonly ILog _logger;

        public AccountController(IMembershipService membershipService, IHubContext hubContext, ILog logger)
        {
            _membershipService = membershipService;
            _appHub = hubContext;
            _logger = logger;
        }

        [Route("login")]
        [HttpPost]
        public HttpResponseMessage Login(HttpRequestMessage request, LoginViewModel loginViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                    return request.CreateResponse(HttpStatusCode.BadRequest,
                        new {success = false, message = "Input all fields."});

                var membershipContext = _membershipService.ValidateUser(loginViewModel.Email, loginViewModel.Password);

                return membershipContext.User == null
                    ? request.CreateResponse(HttpStatusCode.OK,
                        new {success = false, message = "Check email and password"})
                    : request.CreateResponse(HttpStatusCode.OK, new {success = true});
            }
            catch (Exception e)
            {
                _logger.Error(e);

                return request.CreateResponse(HttpStatusCode.OK, new {success = false, message = "Can't login."});
            }
        }

        [Route("register")]
        [HttpPost]
        public HttpResponseMessage Register(HttpRequestMessage request, RegistrationViewModel registrationViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                    return request.CreateResponse(HttpStatusCode.BadRequest,
                        new {success = false, message = "Input all fields."});

                _membershipService.GetUserByEmail(registrationViewModel.Email);

                return request.CreateResponse(HttpStatusCode.OK,
                    new {success = false, message = "You can't use this login."});
            }
            catch
            {
                var user = _membershipService.CreateUser(registrationViewModel.Email, registrationViewModel.Password);
                _appHub.Clients.All.addUser(Mapper.Map<UserViewModel>(user));

                return user != null
                    ? request.CreateResponse(HttpStatusCode.OK, new {success = true})
                    : request.CreateResponse(HttpStatusCode.OK, new {success = false, message = "Can't create user."});
            }
        }

        [HttpGet]
        public HttpResponseMessage Get(HttpRequestMessage request, string email)
        {
            try
            {
                return request.CreateResponse(HttpStatusCode.OK,
                    new {success = true, isAdmin = _membershipService.IsUserAdmin(email)});
            }
            catch (Exception e)
            {
                _logger.Error(e);

                return request.CreateResponse(HttpStatusCode.OK,
                    new {success = false, message = "Can't check user's rights."});
            }
        }

        [Route("refillbalance")]
        [HttpPost]
        public HttpResponseMessage RefillBalance(HttpRequestMessage request,
            [FromBody] UpdateBalanceViewModel updateBalanceViewModel)
        {
            try
            {
                var user = _membershipService.GetUserByEmail(updateBalanceViewModel.Email);
                _membershipService.UpdateUserBalance(user, updateBalanceViewModel.Amount, true);

                return request.CreateResponse(HttpStatusCode.OK,
                    new {success = true, amount = updateBalanceViewModel.Amount, add = true});
            }
            catch (Exception e)
            {
                _logger.Error(e);

                return request.CreateResponse(HttpStatusCode.OK, new {success = false, message = "Can't refill balance"});
            }
        }
    }
}
