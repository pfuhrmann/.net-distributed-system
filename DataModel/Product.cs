namespace DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Product
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

        public decimal Weight { get; set; }

        public short BoxItemsAmount { get; set; }

        public int StockTotal { get; set; }

        public virtual ObservableListSource<OrderItem> OrderItems { get; set; }

        public virtual ObservableListSource<Stock> Stocks { get; set; }

        public virtual ObservableListSource<StocksTransferItem> StocksTransferItems { get; set; }
    }
}
