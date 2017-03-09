using OnlineMarket.DataAccessLayer.Entities;
using System.Collections.Generic;

namespace OnlineMarket.Models
{
    public class OperationsViewModel
    {
        public IList<UserResources> ResourcesToBuyList { get; set; }

        public IList<UserResources> ResourcesToSaleList { get; set; }

        public double Balance { get; set; }

        public double Profit { get; set; }
    }
}