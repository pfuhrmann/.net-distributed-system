using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataModel
{
    public class CartItem
    {
        public int Id { get; set; }

        [Required]
        public int Quantity { get; set; }

        public string CartId { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        [NotMapped]
        public int ItemPrice
        {
            get { return (Product.Price*Quantity); }
        }
    }
}
