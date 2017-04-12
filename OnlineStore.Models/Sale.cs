
namespace OnlineStore.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Sale
    {
        public Sale()
        {
            this.Products = new HashSet<ProductSale>();
        }
        public int SaleId { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        public virtual ICollection<ProductSale> Products { get; set; }
        public decimal TotalSum { get; set; }
        [Required]
        public bool IsDelivered { get; set; }
        [Required]
        public DateTime OrderDate { get; set; }
        public decimal Profit { get; set; }
    }
}
