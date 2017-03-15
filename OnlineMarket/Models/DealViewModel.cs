namespace OnlineMarket.Models
{
    public class DealViewModel
    {
        public string Email { get; set; }
       
        public int ResourceId{ get; set; }

        public string ResourceTitle { get; set; }

        public int Quantity { get; set; }

        public double Price { get; set; }

        public bool IsPurchase { get; set; }
    }
}