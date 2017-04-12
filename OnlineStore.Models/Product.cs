
namespace OnlineStore.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Product
    {
        public Product()
        {
            this.Sales = new HashSet<ProductSale>();
        }
        public int ProductId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public string Unit { get; set; }
        [Required]
        public int Quantity { get; set; }
        public int SupplierId { get; set; }
        [ForeignKey("SupplierId")]
        public virtual Supplier Supplier { get; set; }
        public decimal? DeliveryPrice { get; set; }
        public virtual ICollection<ProductSale> Sales { get; set; }
        public virtual ICollection<ProductDelivery> Deliveries { get; set; }

    }
}
