namespace OrderManagementService
{
    public interface IOrderManagementService
    {
        IOrderWrapper[] GetOrders();
        IOrderWrapper FindOrder(int id);
        bool UpdateOrder(int orderId, string status);
    }
}