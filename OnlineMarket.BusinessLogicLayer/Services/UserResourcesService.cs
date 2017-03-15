using OnlineMarket.BusinessLogicLayer.Interfaces;
using OnlineMarket.DataAccessLayer.Interfaces;
using OnlineMarket.DataAccessLayer.Entities;
using System.Linq;
using System.Collections.Generic;

namespace OnlineMarket.BusinessLogicLayer.Services
{
    public class UserResourcesService : IUserResourcesService
    {
        private IUnitOfWork _unitOfWork;

        public UserResourcesService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IList<UserResources> GetUserResources(string email)
        {
            return _unitOfWork.UserResourcesRepository.Find(ur => ur.User.Email == email).ToList();
        }

        public void UpdateUserResources(UserResources item, bool isPurchase)
        {
            var userResource = _unitOfWork.UserResourcesRepository.Find(ur => ur.UserId == item.UserId && ur.ResourceId == item.ResourceId).First();
            userResource.Quantity = isPurchase ? userResource.Quantity + item.Quantity : userResource.Quantity - item.Quantity;
            _unitOfWork.UserResourcesRepository.Update(userResource);
            _unitOfWork.SaveChanges();
        }
    }
}
