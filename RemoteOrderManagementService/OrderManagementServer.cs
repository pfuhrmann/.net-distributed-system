using System;
using System.Linq;
using DataModel;
using OrderManagementService;

namespace RemoteOrderManagementService
{
    internal class OrderManagementServer : MarshalByRefObject, IOrderManagementService
    {
        private readonly DbModel _context = new DbModel();

        // Get all orders from DB
        public IOrderWrapper[] GetOrders()
        {
            var orders = _context.Orders.ToArray();
            var ordersCount = orders.Count();
            var ordersWrapped = new Order[ordersCount];

            // Map orders arr to serializable wrapper arr
            for (var i = 0; i < ordersCount; i++)
            {
                var order = orders[i];
                ordersWrapped[i] = new Order
                {
                    Id = order.Id,
                    Status = order.Status,
                    OrderTotal = order.OrderTotal,
                    CreatedDateTime = order.CreatedDateTime
                };
            }

            return ordersWrapped;
        }

        // Find order by ID
        public IOrderWrapper FindOrder(int id)
        {
            var order = _context.Orders.Find(id);

            if (order != null)
            {
                return new Order
                {
                    Id = order.Id,
                    Status = order.Status,
                    OrderTotal = order.OrderTotal,
                    CreatedDateTime = order.CreatedDateTime
                };
            }

            return null;
        }

        // Update order status
        public bool UpdateOrder(int orderId, string status)
        {
            var order = _context.Orders.Find(orderId);
            order.Status = status;
            var savedCount = _context.SaveChanges();

            return (savedCount == 1);
        }
    }
}