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
    
    public partial class Product
    {
        public Product()
        {
            this.BasketItems = new HashSet<BasketItem>();
            this.OrderItems = new HashSet<OrderItem>();
            this.Stocks = new HashSet<Stock>();
            this.StocksTransferItems = new HashSet<StocksTransferItem>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public short Price { get; set; }
        public short Weight { get; set; }
        public short BoxItemsAmount { get; set; }
        public int StockReserved { get; set; }
    
        public virtual ICollection<BasketItem> BasketItems { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
        public virtual ICollection<Stock> Stocks { get; set; }
        public virtual ICollection<StocksTransferItem> StocksTransferItems { get; set; }
    }
}
