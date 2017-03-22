using OnlineMarket.DataAccessLayer.Entities;
using System.Collections.Generic;

namespace OnlineMarket.BusinessLogicLayer.Interfaces
{
    public interface IDealService
    {
        IList<Deal> GetDealsByUser(string email);

        IList<Deal> GetActivities(int count);

        void AddPurchaseDeal(Deal deal);

        void AddSaleDeal(Deal deal);

        double[] GetProfits(string email);

        void UpdateUserBalance(User user, double amount, bool isPurchase);
    }
}
