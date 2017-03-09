using System.ComponentModel.DataAnnotations;

namespace OnlineMarket.DataAccessLayer.Entities
{
    public class UserResources
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }

        public double Price { get; set; }
    }
}
