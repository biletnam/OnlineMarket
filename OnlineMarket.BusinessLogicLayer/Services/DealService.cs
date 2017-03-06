using OnlineMarket.BusinessLogicLayer.Interfaces;
using OnlineMarket.DataAccessLayer.Entities;
using OnlineMarket.DataAccessLayer.Interfaces;
using System.Collections.Generic;

namespace OnlineMarket.BusinessLogicLayer.Services
{
    public class DealService : IDealService
    {
        private IUnitOfWork _unitOfWork;

        private enum DealTypes { Purchase = 1, Sale}
        public DealService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void AddPurchaseDeal(Deal deal)
        {
            deal.DealTypeId = (int)DealTypes.Purchase;
            _unitOfWork.DealRepository.Add(deal);
            _unitOfWork.SaveChanges();
        }

        public void AddSaleDeal(Deal deal)
        {
            deal.DealTypeId = (int)DealTypes.Sale;
            _unitOfWork.DealRepository.Add(deal);
            _unitOfWork.SaveChanges();
        }

        public IList<Deal> GetDeals()
        {
            return _unitOfWork.DealRepository.GetAll();
        }

        public IList<Deal> GetDealsByUserId(int userId)
        {
            return _unitOfWork.DealRepository.Find(d => d.UserId == userId);
        }
    }
}
