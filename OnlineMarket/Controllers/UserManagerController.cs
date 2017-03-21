using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using log4net;
using OnlineMarket.BusinessLogicLayer.Interfaces;
using OnlineMarket.Core;
using OnlineMarket.Models;

namespace OnlineMarket.Controllers
{
    public class UserManagerController : ApiController
    {
        private readonly IMembershipService _membershipService;

        private readonly ILog _logger;

        public UserManagerController(IMembershipService membershipService, ILog logger)
        {
            _membershipService = membershipService;
            _logger = logger;
        }

        [HttpGet]
        public HttpResponseMessage Get(HttpRequestMessage request)
        {
            try
            {
                var users = Mapper.Map<IList<UserViewModel>>(_membershipService.GetUsers());
                return request.CreateResponse(HttpStatusCode.OK, new {success = true, users});
            }
            catch (Exception e)
            {
                _logger.Error(e);
                return request.CreateResponse(HttpStatusCode.OK, new {success = false, message = Messages.CantLoadUsers});
            }
        }

        [HttpPost]
        public HttpResponseMessage Post(HttpRequestMessage request, [FromBody] UserViewModel userViewModel)
        {
            try
            {
                _membershipService.ChangeUserRole(userViewModel.Id, userViewModel.RoleId);
                return request.CreateResponse(HttpStatusCode.OK, new {success = true});
            }
            catch (Exception e)
            {
                _logger.Error(e);
                return request.CreateResponse(HttpStatusCode.OK, new {success = false, message = Messages.CantChangeRole});
            }
        }
    }
}
