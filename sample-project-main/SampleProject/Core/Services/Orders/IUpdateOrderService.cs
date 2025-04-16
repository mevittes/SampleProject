using System;
using System.Collections.Generic;
using BusinessEntities;

namespace Core.Services.Orders
{
    public interface IUpdateOrderService
    {
        void Update(Order order, Guid userId, DateTime? date, IEnumerable<Guid> productIds);
        IEnumerable<string> ValidateModelInputs(Guid userId, DateTime? date, IEnumerable<Guid> productIds);
    }
}