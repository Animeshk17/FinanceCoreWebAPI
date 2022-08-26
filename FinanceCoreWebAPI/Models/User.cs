using System.ComponentModel.DataAnnotations;

namespace FinanceCoreWebAPI.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Account_number { get; set; }
        public string Ifsc_code { get; set; }
        public bool Is_verified { get; set; }
    }
}
