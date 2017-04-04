
namespace OnlineStore.Models
{
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
    public class Supplier
    {
        public Supplier()
        {
            this.Products = new HashSet<Product>();
        }
        public int SupplierId { get; set; }
        [Required]
        public string Name { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
