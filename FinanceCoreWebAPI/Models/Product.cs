using System.ComponentModel.DataAnnotations;

namespace FinanceCoreWebAPI.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public decimal ProductPrice { get; set; }
        public string ProductImageAddress { get; set; }
    }
}
