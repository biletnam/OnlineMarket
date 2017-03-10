using AutoMapper;
using OnlineMarket.BusinessLogicLayer.Interfaces;
using OnlineMarket.DataAccessLayer.Entities;
using OnlineMarket.Models;
using System.Collections.Generic;
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
        public IList<UserViewModel> Get()
        {
            var users = Mapper.Map<IList<UserViewModel>>(_membershipService.GetUsers());
            return Mapper.Map<IList<UserViewModel>>(_membershipService.GetUsers());
        }

        [HttpPost]
        public void Post([FromBody]UserViewModel userViewModel)
        {
            _membershipService.ChangeUserRole(userViewModel.Id, userViewModel.RoleId);
        }
    }
}
