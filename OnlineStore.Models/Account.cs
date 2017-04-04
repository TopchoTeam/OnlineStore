
namespace OnlineStore.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Account
    {
        public int AccountId { get; set; }
        [Required]
        public string AccountNumber { get; set; }
        [Required]
        public decimal Balance { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}
