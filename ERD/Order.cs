//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ClassLibrary2
{
    using System;
    using System.Collections.Generic;
    
    public partial class Order
    {
        public Order()
        {
            this.OrderItems = new HashSet<OrderItem>();
        }
    
        public int Id { get; set; }
        public System.DateTime CreatedDateTime { get; set; }
        public string Status { get; set; }
        public int OrderTotal { get; set; }
        public string CustomerId { get; set; }
        public int DestinationWarehouseId { get; set; }
    
        public virtual AspNetUser AspNetUser { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
        public virtual Warehouse Warehouse { get; set; }
    }
}
