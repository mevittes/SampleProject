using BusinessEntities;
using System;

namespace WebApi.Models.Orders
{
    public class OrderData : IdObjectData
    {
        public OrderData(Order order) : base(order)
        {
            Date = order.Date;
            UserId = order.UserId;
        }

        public DateTime Date { get; set; }
        public Guid UserId { get; set; }
    }
}