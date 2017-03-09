namespace OnlineMarket.Models
{
    public class DealViewModel
    {
        public int UserId { get; set; }
       
        public int ResourceId{ get; set; }

        public int Quantity { get; set; }

        public double Amount { get; set; }

        public bool IsPurchase { get; set; }
    }
}