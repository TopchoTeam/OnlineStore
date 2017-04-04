
namespace OnlineStore.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public enum UserRole
    {
        Customer,
        Admin
    }
    public class User
    {
        public User()
        {
            this.Sales = new HashSet<Sale>();
        }
        public int UserId { get; set; }
        [MinLength(3),MaxLength(15)]
        public string UserName { get; set; }
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public int AccountId { get; set; }
        [ForeignKey("AccountId")]
        public virtual Account Account { get; set; }
        [Required]
        public string Address { get; set; }
        public bool IsDeleted { get; set; }
        [Required]
        public UserRole Role { get; set; }
        public virtual ICollection<Sale> Sales { get; set; }

    }
}
