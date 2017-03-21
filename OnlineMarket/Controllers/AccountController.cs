using System;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using log4net;
using Microsoft.AspNet.SignalR;
using OnlineMarket.BusinessLogicLayer.Interfaces;
using OnlineMarket.Models;
using OnlineMarket.Utilities.Interfaces;

namespace OnlineMarket.Controllers
{
    [RoutePrefix("api/Account")]
    public class AccountController : ApiController
    {
        private readonly IHubContext _appHub;

        private readonly ILog _logger;

        private readonly IMembershipService _membershipService;

        private readonly ISendEmailService _sendEmailService;

        public AccountController(IMembershipService membershipService, IHubContext hubContext, ILog logger,
            ISendEmailService sendEmailService)
        {
            _membershipService = membershipService;
            _appHub = hubContext;
            _logger = logger;
            _sendEmailService = sendEmailService;
        }

        [Route("login")]
        [HttpPost]
        public HttpResponseMessage Login(HttpRequestMessage request, LoginViewModel loginViewModel)
        {
            try
            {
                if (!ModelState.IsValid) return ReturnFalseResponse(request, "Input all fields.");

                var membershipContext = _membershipService.ValidateUser(loginViewModel.Email, loginViewModel.Password);

                return ReturnResponse(membershipContext.User == null, request, "Check email and password.");
            }
            catch (Exception e)
            {
                _logger.Error(e);

                return ReturnFalseResponse(request, "Can't login.");
            }
        }

        [Route("register")]
        [HttpPost]
        public HttpResponseMessage Register(HttpRequestMessage request, RegistrationViewModel registrationViewModel)
        {
            try
            {
                if (!ModelState.IsValid) return ReturnFalseResponse(request, "Input all fields.");

                if (_membershipService.GetUserByEmail(registrationViewModel.Email) != null)
                    return ReturnFalseResponse(request, "You can't use this login.");

                var user = _membershipService.CreateUser(registrationViewModel.Email, registrationViewModel.Password);
                _sendEmailService.Send(user.Email, GetConfirmLink(user.Email, user.ConfirmCode));
                _appHub.Clients.All.addUser(Mapper.Map<UserViewModel>(user));

                return request.CreateResponse(HttpStatusCode.OK, new {success = true});
            }
            catch (Exception e)
            {
                _logger.Error(e);

                return ReturnFalseResponse(request, "Can't create user.");
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

                return ReturnFalseResponse(request, "Can't check user's rights.");
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

                return ReturnFalseResponse(request, "Can't refill balance");
            }
        }

        [Route("confirmemail")]
        [HttpGet]
        public HttpResponseMessage ConfirmEmail(HttpRequestMessage request, string email, string code)
        {
            try
            {
                var membershipContext = _membershipService.ConfirmEmail(email, code);

                return ReturnResponse(membershipContext.User == null, request, null);
            }
            catch (Exception e)
            {
                _logger.Error(e);

                return ReturnFalseResponse(request, null);
            }
        }

        private string GetConfirmLink(string email, string code)
        {
            return $"{ConfigurationManager.AppSettings["link"]}/confirm/{email}/{code};";
        }

        private HttpResponseMessage ReturnResponse(bool condition, HttpRequestMessage request, string falseMessage)
        {
            return condition
                ? ReturnFalseResponse(request, falseMessage)
                : request.CreateResponse(HttpStatusCode.OK, new {success = true});
        }

        private HttpResponseMessage ReturnFalseResponse(HttpRequestMessage request, string falseMessage)
        {
            return request.CreateResponse(HttpStatusCode.OK,
                new {success = false, message = falseMessage});
        }
    }
}