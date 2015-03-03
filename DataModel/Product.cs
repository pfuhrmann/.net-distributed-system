using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace DataModel
{
    public class Product
    {
        public Product()
        {
            OrderItems = new ObservableListSource<OrderItem>();
            Stocks = new ObservableListSource<Stock>();
            StocksTransferItems = new ObservableListSource<StocksTransferItem>();
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public short Price { get; set; }
        public short Weight { get; set; }
        public short BoxItemsAmount { get; set; }

        [NotMapped]
        public int StockTotal
        {
            get { return Stocks.Sum(s => s.Quantity); }
        }

        public virtual ObservableListSource<OrderItem> OrderItems { get; set; }
        public virtual ObservableListSource<Stock> Stocks { get; set; }
        public virtual ObservableListSource<StocksTransferItem> StocksTransferItems { get; set; }
    }
}
