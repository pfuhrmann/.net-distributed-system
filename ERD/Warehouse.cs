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
    
    public partial class Warehouse
    {
        public Warehouse()
        {
            this.Orders = new HashSet<Order>();
            this.Stocks = new HashSet<Stock>();
            this.StocksTransfers = new HashSet<StocksTransfer>();
            this.StocksTransfers1 = new HashSet<StocksTransfer>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
    
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Stock> Stocks { get; set; }
        public virtual ICollection<StocksTransfer> StocksTransfers { get; set; }
        public virtual ICollection<StocksTransfer> StocksTransfers1 { get; set; }
    }
}
