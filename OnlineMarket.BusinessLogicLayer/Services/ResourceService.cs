using OnlineMarket.BusinessLogicLayer.Interfaces;
using OnlineMarket.DataAccessLayer.Entities;
using OnlineMarket.DataAccessLayer.Interfaces;
using System.Collections.Generic;

namespace OnlineMarket.BusinessLogicLayer.Services
{
    public class ResourceService : IResourceService
    {
        private IUnitOfWork _unitOfWork;

        public ResourceService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IList<UserResources> GetResources()
        {
            return _unitOfWork.ResourceRepository.GetAll();
        }

        public IList<UserResources> GetResourcesByUser(string email)
        {
            return _unitOfWork.UserResourcesRepository.Find(ur => ur.User.Email == email);
        }

        public void ModifyResourcePrice(UserResources resource, double price)
        {
            resource.Price = price;
            _unitOfWork.ResourceRepository.Update(resource);
            _unitOfWork.SaveChanges();
        }
    }
}
