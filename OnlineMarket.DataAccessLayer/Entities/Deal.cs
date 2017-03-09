using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineMarket.DataAccessLayer.Entities
{
    public class Deal
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        public int ResourceId { get; set; }

        [ForeignKey("ResourceId")]
        public virtual UserResources Resource { get; set; }

        public int DealTypeId { get; set; }

        [ForeignKey("DealTypeId")]
        public virtual DealType DealType { get; set; }

        public int Quantity { get; set; }

        public double Amount { get; set; }

        public DateTime Date { get; set; }
    }
}
