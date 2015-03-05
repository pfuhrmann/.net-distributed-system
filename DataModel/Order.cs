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
        public DateTime Date { get; set; }

        [Required]
        public string Status { get; set; }

        [Required]
        public int OrderTotal { get; set; }

        public string CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual ObservableListSource<OrderItem> OrderItems { get; set; }
    }
}
