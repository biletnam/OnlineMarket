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

        public IList<Resource> GetResources()
        {
            return _unitOfWork.ResourceRepository.GetAll();
        }
        
        public void AddResource(Resource resource)
        {
            _unitOfWork.ResourceRepository.Add(resource);
            foreach(var user in _unitOfWork.UserRepository.GetAll())
            {
                _unitOfWork.UserResourcesRepository.Add(new UserResources { UserId = user.Id, ResourceId = resource.Id, Quantity = 0 });
            }
            _unitOfWork.SaveChanges();
        }

        public void ModifyResourcePrice(Resource resource, double price)
        {
            resource.Price = price;
            _unitOfWork.ResourceRepository.Update(resource);
            _unitOfWork.SaveChanges();
        }
    }
}
