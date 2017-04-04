namespace OnlineStore.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class ProductSale
    {
        [Key]
        [Column(Order =1)]
        public int ProductId { get; set; }
        
        public virtual Product Product { get; set; }

        [Key]
        [Column(Order =2)]
        public int SaleId { get; set; }
        
        public virtual Sale Sale { get; set; }

        public int OrderedQuantity { get; set; }
    }
}
