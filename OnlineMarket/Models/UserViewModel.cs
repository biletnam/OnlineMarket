namespace OnlineMarket.Models
{
    public class UserViewModel
    {
        public int Id { get; set; }

        public string Email { get; set; }
        
        public double Balance { get; set; }

        public string RoleTitle { get; set; }

        public int RoleId { get; set; }
    }
}