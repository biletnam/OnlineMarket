using System.Collections.Generic;
using OnlineMarket.BusinessLogicLayer.Interfaces;
using OnlineMarket.DataAccessLayer.Entities;
using OnlineMarket.DataAccessLayer.Interfaces;

namespace OnlineMarket.BusinessLogicLayer.Services
{
    public class ResourceService : IResourceService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ResourceService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IList<Resource> GetResources()
        {
            return _unitOfWork.ResourceRepository.GetAll();
        }

        public void AddResource(Resource resource)
        {
            _unitOfWork.ResourceRepository.Add(resource);

            foreach (var user in _unitOfWork.UserRepository.GetAll())
            {
                _unitOfWork.UserResourcesRepository.Add(CreateUserResource(user.Id, resource.Id));
            }

            _unitOfWork.SaveChanges();
        }

        public void ModifyResourcePrice(Resource resource, double price)
        {
            resource.Price = price;
            _unitOfWork.ResourceRepository.Update(resource);
            _unitOfWork.SaveChanges();
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
