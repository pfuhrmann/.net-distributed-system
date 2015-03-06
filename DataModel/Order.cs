using System;
using System.ComponentModel.DataAnnotations;

namespace DataModel
{
    public class Order
    {
        public Order()
        {
            OrderItems = new ObservableListSource<OrderItem>();
        }

        public int Id { get; set; }

        [Required]
        [Display(Name = "Order Placed")]
        [DisplayFormat(DataFormatString = "{0:D}")]
        public DateTime CreatedDateTime { get; set; }

        [Required]
        public string Status { get; set; }

        [Required]
        [Display(Name = "Total")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public int OrderTotal { get; set; }

        public string CustomerId { get; set; }
        public int DestinationWarehouseId { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Warehouse DestinationWarehouse { get; set; }
        public virtual ObservableListSource<OrderItem> OrderItems { get; set; }
    }
}
