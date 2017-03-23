using System.Collections.Generic;
using System.Linq;
using OnlineMarket.BusinessLogicLayer.Interfaces;
using OnlineMarket.DataAccessLayer.Entities;
using OnlineMarket.DataAccessLayer.Interfaces;

namespace OnlineMarket.BusinessLogicLayer.Services
{
    public class UserResourcesService : IUserResourcesService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserResourcesService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IList<UserResources> GetUserResources(string email)
        {
            return _unitOfWork.UserResourcesRepository.Find(ur => ur.User.Email == email).ToList();
        }

        public void AddUserResources(User user)
        {
            foreach (var resource in _unitOfWork.ResourceRepository.GetAll())
            {
                _unitOfWork.UserResourcesRepository.Add(CreateUserResource(user.Id, resource.Id));
            }
        }

        private UserResources CreateUserResource(int userId, int resourceId)
        {
            return new UserResources
            {
                UserId = userId,
                ResourceId = resourceId,
                Quantity = 0
            };
        }
    }
}
