using System.ComponentModel.DataAnnotations;
using System.Linq;

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

        public bool ItemsAvalable(int productId, int quantity)
        {
            return Stocks.Any(s => s.ProductId == productId && s.Quantity >= quantity);
        }
    }
}