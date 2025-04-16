using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Xml.Linq;
using BusinessEntities;
using Common;

namespace Core.Services.Orders
{
    [AutoRegister(AutoRegisterTypes.Singleton)]
    public class UpdateOrderService : IUpdateOrderService
    {
        public void Update(Order order, Guid userId, DateTime? date, IEnumerable<Guid> productIds)
        {
            order.SetUserId(userId);
            order.SetDate(date);
            order.SetProductIds(productIds);
        }

        public IEnumerable<string> ValidateModelInputs(Guid userId, DateTime? date, IEnumerable<Guid> productIds)
        {
            var validationErrors = new List<string>();
            if (!date.HasValue)
            {
                validationErrors.Add("Date was not provided.");
            }
            return validationErrors;
        }
    }
}