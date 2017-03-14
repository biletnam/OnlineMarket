using OnlineMarket.BusinessLogicLayer.Interfaces;
using OnlineMarket.DataAccessLayer.Entities;
using OnlineMarket.DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

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

            foreach(var deal in deals)
            {
                var i = resources.IndexOf(resources.Where(r => r.Id == deal.ResourceId).First());
                profits[i] = deal.DealTypeId == (int)DealTypes.Purchase ? profits[i] - deal.Amount : profits[i] + deal.Amount;
                
            }

            return profits;
        }
    }
}
