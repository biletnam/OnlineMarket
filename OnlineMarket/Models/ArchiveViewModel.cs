using System;

namespace OnlineMarket.Models
{
    public class ArchiveViewModel
    {
        public string ResourceTitle { get; set; }

        public int Quantity { get; set; }

        public double Price { get; set; }

        public string DealType { get; set; }

        public DateTime Date { get; set; }
    }
}