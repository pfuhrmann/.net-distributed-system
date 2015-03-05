using System.ComponentModel.DataAnnotations;

namespace DataModel
{
    public class Warehouse
    {
        public Warehouse()
        {
            Stocks = new ObservableListSource<Stock>();
            StocksTransfers = new ObservableListSource<StocksTransfer>();
            StocksTransfers1 = new ObservableListSource<StocksTransfer>();
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual ObservableListSource<Stock> Stocks { get; set; }
        public virtual ObservableListSource<StocksTransfer> StocksTransfers { get; set; }
        public virtual ObservableListSource<StocksTransfer> StocksTransfers1 { get; set; }
    }
}
