//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace StockManagement
{
    using System;
    using System.Collections.Generic;
    
    public partial class StockTransferItem
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
    
        public virtual Product Product { get; set; }
        public virtual StockTransfer StockTransfer { get; set; }
    }
}
