using System.Collections.Generic;
using System.Linq;
using OnlineMarket.BusinessLogicLayer.Interfaces;
using OnlineMarket.DataAccessLayer.Entities;
using OnlineMarket.DataAccessLayer.Interfaces;

namespace OnlineMarket.BusinessLogicLayer.Services
{
    public class DealService : IDealService
    {
        private readonly IUnitOfWork _unitOfWork;

        private enum DealTypes
        {
            Purchase = 1,
            Sale
        }

        public DealService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void AddPurchaseDeal(Deal deal)
        {
            if (deal.Amount > deal.User.Balance) return;
            
            AddDeal(deal, (int)DealTypes.Purchase);
        }

        public void AddSaleDeal(Deal deal)
        {
            AddDeal(deal, (int)DealTypes.Sale);
        }

        public void UpdateUserBalance(User user, double amount, bool isPurchase)
        {
            user.Balance = isPurchase ? user.Balance - amount : user.Balance + amount;
            _unitOfWork.UserRepository.Update(user);
            _unitOfWork.SaveChanges();
        }


        public IList<Deal> GetActivities(int count)
        {
            var activities = _unitOfWork.DealRepository.GetAll();

            return activities.Skip(activities.Count - count).ToList();
        }

        public IList<Deal> GetDealsByUser(string email)
        {
            return _unitOfWork.DealRepository.Find(d => d.User.Email == email);
        }

        public double[] GetProfits(string email)
        {
            var deals = GetDealsByUser(email);
            var resources = _unitOfWork.ResourceRepository.GetAll();
            var profits = new double[resources.Count];

            foreach (var deal in deals)
            {
                var i = resources.IndexOf(resources.First(r => r.Id == deal.ResourceId));
                profits[i] = deal.DealTypeId == (int) DealTypes.Purchase
                    ? profits[i] - deal.Amount
                    : profits[i] + deal.Amount;
            }

            return profits;
        }

        private void AddDeal(Deal deal, int dealType)
        {
            deal.DealTypeId = dealType;
            _unitOfWork.DealRepository.Add(deal);
            UpdateUserResources(deal, deal.UserId, deal.DealTypeId == (int)DealTypes.Purchase);
            UpdateUserBalance(deal.User, deal.Amount, deal.DealTypeId == (int)DealTypes.Purchase);
        }

        private void UpdateUserResources(Deal deal, int userId, bool isPurchase)
        {
            var userResource =
                _unitOfWork.UserResourcesRepository.Find(
                    ur => ur.UserId == userId && ur.ResourceId == deal.ResourceId).First();

            userResource.Quantity = isPurchase
                ? userResource.Quantity + deal.Quantity
                : userResource.Quantity - deal.Quantity;

            _unitOfWork.UserResourcesRepository.Update(userResource);
        }
    }
}
