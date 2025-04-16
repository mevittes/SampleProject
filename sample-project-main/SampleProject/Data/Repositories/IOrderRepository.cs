using System;
using System.Collections.Generic;
using BusinessEntities;

namespace Data.Repositories
{
    public interface IOrderRepository
    {
        IEnumerable<Order> GetOrders(Guid? userId = null, DateTime? date = null, Guid? productId = null);
        void DeleteAll();
        void Save(Order order);
        void Delete(Order order);
        Order Get(Guid id);
    }
}