namespace DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Order
    {
        public Order()
        {
            OrderItems = new ObservableListSource<OrderItem>();
        }

        public int Id { get; set; }

        [Required]
        public string Date { get; set; }

        [Required]
        public string Status { get; set; }

        public int Customer_Id { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual ObservableListSource<OrderItem> OrderItems { get; set; }
    }
}
