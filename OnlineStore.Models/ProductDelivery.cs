
namespace OnlineStore.Models
{
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
    public class ProductDelivery
    {

        [Key]
        [Column(Order = 1)]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        [Key]
        [Column(Order = 2)]
        public int DeliveryId { get; set; }
        public virtual Delivery Delivery { get; set; }
        public int DeliveredQuantity { get; set; }
    }
}
