using System.ComponentModel.DataAnnotations;

namespace OnlineMarket.DataAccessLayer.Entities
{
    public class DealType
    {
        [Key]
        public int Id { get; set; }

        public string Type { get; set; }
    }
}
