using OnlineMarket.DataAccessLayer.Entities;
using System.Collections.Generic;

namespace OnlineMarket.BusinessLogicLayer.Interfaces
{
    public interface IDealService
    {
        IList<Deal> GetDealsByUser(string email);

        void AddPurchaseDeal(Deal deal);

        void AddSaleDeal(Deal deal);
    }
}
