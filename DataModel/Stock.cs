using System.ComponentModel.DataAnnotations;

namespace DataModel
{
    public class Stock
    {
        public int Id { get; set; }

        [Required]
        public int Quantity { get; set; }

        public int Product_Id { get; set; }
        public int Warehouse_Id { get; set; }
        public virtual Product Product { get; set; }
        public virtual Warehouse Warehouse { get; set; }
    }
}
