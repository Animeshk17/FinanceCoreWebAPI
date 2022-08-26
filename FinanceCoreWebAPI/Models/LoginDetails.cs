using System.ComponentModel.DataAnnotations;

namespace FinanceCoreWebAPI.Models
{
    public class LoginDetails
    {
        [Key]
        public int LoginId { get; set; }
        public int UserId { get; set; }
        public string UserEmail { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
