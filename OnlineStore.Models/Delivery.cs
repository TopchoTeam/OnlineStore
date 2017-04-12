using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Models
{
    public class Delivery
    {
        public Delivery()
        {
            this.Products = new HashSet<ProductDelivery>();
        }
        public int DeliveryId { get; set; }
        public int SupplierId { get; set; }
        [ForeignKey("SupplierId")]
        public virtual Supplier Supplier { get; set; }
        [Required]
        public DateTime DeliveryDate { get; set; }
        public virtual ICollection<ProductDelivery> Products { get; set; }
        public decimal TotalSum { get; set; }


    }
}
