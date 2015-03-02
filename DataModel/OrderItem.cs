namespace DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OrderItem
    {
        public int Id { get; set; }

        public int Quantity { get; set; }

        [Required]
        public string Status { get; set; }

        public int Order_Id { get; set; }

        public int Product_Id { get; set; }

        public virtual Order Order { get; set; }

        public virtual Product Product { get; set; }
    }
}
