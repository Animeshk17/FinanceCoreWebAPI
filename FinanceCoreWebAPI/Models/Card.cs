using System;
using System.ComponentModel.DataAnnotations;

namespace FinanceCoreWebAPI.Models
{
    public class Card
    {
        [Key]
        public int CardId { get; set; }
        public int UserId { get; set; }
        public string CardType { get; set; }
        public decimal AccountBalance { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}
