//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class Order
    {
        public Order()
        {
            this.OrderItems = new ObservableListSource<OrderItem>();
        }
    
        public int Id { get; set; }
        public string Date { get; set; }
        public string Status { get; set; }
    
        public virtual Customer Customer { get; set; }
        public virtual ObservableListSource<OrderItem> OrderItems { get; set; }
    }
}