using System;
using System.Collections.Generic;
using System.Linq;
using BusinessEntities;
using Common;
using Microsoft.Extensions.Caching.Memory;

namespace Data.Repositories
{
    [AutoRegister]
    public class OrderRepository : IOrderRepository
    {
        private readonly IInternalCache _memoryCache;

        private const string _cacheKeyForOrder = "OrdersDictionary";

        public OrderRepository(IInternalCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public IEnumerable<Order> GetOrders(Guid? userId = null, DateTime? date = null, Guid? productId = null)
        {
            
            IEnumerable<Order> orders = GetOrderData().Values;
            if (userId != null)
            {
                orders = orders.Where(o => o.UserId == userId);
            }
            if (date != null)
            {
                orders = orders.Where(o => o.Date == date);
            }
            if (productId != null)
            {
                orders = orders.Where(o => o.ProductIds.Contains(productId.Value));
            }
            return orders.ToList();
        }

        private IDictionary<Guid, Order> GetOrderData()
        {
            if (_memoryCache.TryGetValue(_cacheKeyForOrder, out var orders))
            {
                return (IDictionary <Guid, Order>)orders;
            }
            var newOrders = new Dictionary<Guid, Order>();
            _memoryCache.Set<IDictionary<Guid, Order>>(_cacheKeyForOrder, newOrders);
            return newOrders;
        }

        public void DeleteAll()
        {
            var orders = new Dictionary<Guid, Order>();
            _memoryCache.Set(_cacheKeyForOrder, orders);
        }

        public void Save(Order order)
        {
            var orders = GetOrderData();
            orders[order.Id] = order;
            _memoryCache.Set(_cacheKeyForOrder, orders);
        }

        public void Delete(Order order)
        {
            var orders = GetOrderData();
            orders.Remove(order.Id);
            _memoryCache.Set(_cacheKeyForOrder, orders);
        }

        public Order Get(Guid id)
        {
            var orders = GetOrderData();
            return orders.ContainsKey(id) ? orders[id] : null;
        }
    }
}