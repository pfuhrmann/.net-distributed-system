namespace OrderManagementService
{
    public interface IOrderManagementService
    {
        IOrderWrapper[] GetOrders();
        bool UpdateOrder(int orderId, string status);
    }
}