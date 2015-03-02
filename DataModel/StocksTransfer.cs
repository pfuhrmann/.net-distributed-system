namespace DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class StocksTransfer
    {
        public StocksTransfer()
        {
            StocksTransferItems = new ObservableListSource<StocksTransferItem>();
        }

        public int Id { get; set; }

        public DateTime Date { get; set; }

        public decimal Distance { get; set; }

        public int Departure_Id { get; set; }

        public int Destination_Id { get; set; }

        public virtual ObservableListSource<StocksTransferItem> StocksTransferItems { get; set; }

        public virtual Warehouse Warehouse { get; set; }

        public virtual Warehouse Warehouse1 { get; set; }
    }
}
