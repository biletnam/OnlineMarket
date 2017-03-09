using System;

namespace OnlineMarket.Models
{
    public class ArchiveViewModel
    {
        public string ResourceTitle { get; set; }

        public int Quantity { get; set; }

        public double Amount { get; set; }

        public string DealType { get; set; }

        public DateTime Date { get; set; }
    }
}