using System.ComponentModel.DataAnnotations;

namespace DataModel
{
    public class OrderItem
    {
        public int Id { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public string Status { get; set; }

        public int Order_Id { get; set; }
        public int Product_Id { get; set; }
        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
    }
}
