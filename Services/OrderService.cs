using System;
using System.Collections.Generic;
using System.Threading;

namespace OrdersManager.Services
{
    public class Order
    {
        public int Id { get; set; }
        public string ItemName { get; set; } = string.Empty;
        public DateTime OrderDate { get; set; } = DateTime.Now;
    }

    public interface IOrderService
    {
        void AddOrder(Order order);
        List<Order> GetOrders();
        Guid GetInstanceId();
        int GetOrdersCount();
    }

    public class OrderService : IOrderService
    {
        private readonly Guid _instanceId;
        private readonly List<Order> _orders;
        private static int _nextOrderId = 0;
        private readonly object _lock = new();

        public OrderService()
        {
            _instanceId = Guid.NewGuid();
            _orders = new List<Order>();
            Console.WriteLine($"OrderService Instanciado. ID: {_instanceId}");
        }

        public void AddOrder(Order order)
        {
            lock (_lock)
            {
                order.Id = Interlocked.Increment(ref _nextOrderId);
                _orders.Add(order);
            }
            Console.WriteLine($"Pedido agregado en {_instanceId}, total: {_orders.Count}");
        }

        public List<Order> GetOrders() => _orders;

        public Guid GetInstanceId() => _instanceId;

        public int GetOrdersCount() => _orders.Count;
    }
}
