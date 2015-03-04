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

        [Required]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public short Price { get; set; }

        [Required]
        public short Weight { get; set; }

        [Required]
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
