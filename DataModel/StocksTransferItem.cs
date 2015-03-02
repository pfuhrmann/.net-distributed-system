namespace DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class StocksTransferItem
    {
        public int Id { get; set; }

        public int Quantity { get; set; }

        public int Product_Id { get; set; }

        public int StockTransfer_Id { get; set; }

        public virtual Product Product { get; set; }

        public virtual StocksTransfer StocksTransfer { get; set; }
    }
}
