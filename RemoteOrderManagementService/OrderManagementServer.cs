using System;
using System.Data.Entity;
using System.Linq;
using DataModel;
using OrderManagementService;

namespace RemoteOrderManagementService
{
    internal class OrderManagementServer : MarshalByRefObject, IOrderManagementService
    {
        private readonly DbModel _context = new DbModel();

        public IOrderWrapper[] GetOrders()
        {
            _context.Orders.Load();
            var orders = _context.Orders.ToArray();
            var ordersWrapped = new Order[orders.Count()];

            for (var i = 0; i < orders.Count(); i++)
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

        public bool UpdateOrder(int orderId, string status)
        {
            var order = _context.Orders.Find(orderId);
            order.Status = status;
            var savedCount = _context.SaveChanges();

            return (savedCount == 1);
        }
    }
}