namespace DataModel
{
    public class StocksTransferItem
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public int StockTransferId { get; set; }
        public virtual Product Product { get; set; }
        public virtual StocksTransfer StocksTransfer { get; set; }
    }
}
