using System;
using OnlineMarket.BusinessLogicLayer.Interfaces;
using OnlineMarket.DataAccessLayer.Interfaces;
using OnlineMarket.DataAccessLayer.Entities;
using System.Linq;
using System.Collections.Generic;

namespace OnlineMarket.BusinessLogicLayer.Services
{
    class UserResourcesService : IUserResourcesService
    {
        private IUnitOfWork _unitOfWork;

        public UserResourcesService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void AddResourceToUser(string email, string resourceTitle)
        {
            //_unitOfWork.UserResourcesRepository.Add(new UserResources { })
        }

        public IList<UserResources> GetUserResources(string email)
        {
            return _unitOfWork.UserResourcesRepository.Find(ur => ur.User.Email == email).ToList();
        }

        public void UpdateResourceAmount()
        {
            throw new NotImplementedException();
        }
    }
}
