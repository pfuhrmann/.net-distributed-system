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
    
    public partial class Product
    {
        public Product()
        {
            this.StockTotal = 0;
            this.Stocks = new ObservableListSource<Stock>();
            this.OrderItems = new ObservableListSource<OrderItem>();
            this.StockTransferItems = new ObservableListSource<StocksTransferItem>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal Weight { get; set; }
        public short BoxItemsAmount { get; set; }
        public int StockTotal { get; set; }
    
        public virtual ObservableListSource<Stock> Stocks { get; set; }
        public virtual ObservableListSource<OrderItem> OrderItems { get; set; }
        public virtual ObservableListSource<StocksTransferItem> StockTransferItems { get; set; }
    }
}