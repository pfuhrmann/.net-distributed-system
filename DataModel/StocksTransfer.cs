using System;

namespace DataModel
{
    public class StocksTransfer
    {
        public StocksTransfer()
        {
            StocksTransferItems = new ObservableListSource<StocksTransferItem>();
        }

        public int Id { get; set; }
        public DateTime Date { get; set; }
        public decimal Distance { get; set; }
        public int DepartureId { get; set; }
        public int DestinationId { get; set; }
        public virtual ObservableListSource<StocksTransferItem> StocksTransferItems { get; set; }
        public virtual Warehouse Warehouse { get; set; }
        public virtual Warehouse Warehouse1 { get; set; }
    }
}
