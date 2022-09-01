using System;
using System.ComponentModel.DataAnnotations;

namespace FinanceCoreWebAPI.Models
{
    public class Transaction
    {
        [Key]
        public int TransactionId { get; set; }
        public int UserId { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public DateTime TransactionDate { get; set; }
        public decimal TransactionAmount { get; set; }
    }
}
