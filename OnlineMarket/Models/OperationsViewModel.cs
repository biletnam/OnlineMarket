using OnlineMarket.DataAccessLayer.Entities;
using System.Collections.Generic;

namespace OnlineMarket.Models
{
    public class OperationsViewModel
    {
        public IList<Resource> ResourcesToBuy { get; set; }

        public IList<UserResources> ResourcesToSell { get; set; }

        public double Balance { get; set; }

        public double Profit { get; set; }
    }
}