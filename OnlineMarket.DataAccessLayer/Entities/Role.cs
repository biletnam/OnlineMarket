using System.ComponentModel.DataAnnotations;

namespace OnlineMarket.DataAccessLayer.Entities
{
    public class Role
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }
    }
}
