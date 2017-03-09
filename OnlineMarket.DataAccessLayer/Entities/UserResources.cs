using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineMarket.DataAccessLayer.Entities
{
    public class UserResources
    {
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        public int ResourceId { get; set; }

        [ForeignKey("ResourceId")]
        public UserResources Resource { get; set; }

        public int Amount { get; set; }
    }
}
