using OnlineMarket.DataAccessLayer.Entities;
using System.Collections.Generic;

namespace OnlineMarket.BusinessLogicLayer.Interfaces
{
    public interface IDealService
    {
        IList<Deal> GetDeals();

        IList<Deal> GetDealsByUserId(int userId);

        void AddPurchaseDeal(Deal deal);

        void AddSaleDeal(Deal deal);
    }
}
