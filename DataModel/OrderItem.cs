using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataModel
{
    public class OrderItem
    {
        public int Id { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public string Status { get; set; }

        [Required]
        public int UnitPrice { get; set; }

        [NotMapped]
        public int ItemTotal
        {
            get { return (Product.Price * Quantity); }
        }

        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
    }
}
