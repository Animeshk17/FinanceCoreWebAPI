using System;
using System.ComponentModel.DataAnnotations;

namespace FinanceCoreWebAPI.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal OrderAmount { get; set; }
    }
}
