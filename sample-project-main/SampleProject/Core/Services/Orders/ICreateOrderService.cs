using System;
using System.Collections.Generic;
using System.Reflection;
using BusinessEntities;

namespace Core.Services.Orders
{
    public interface ICreateOrderService
    {
        Order Create(Guid id, Guid userId, DateTime? date, IEnumerable<Guid> productIds);
    }
}