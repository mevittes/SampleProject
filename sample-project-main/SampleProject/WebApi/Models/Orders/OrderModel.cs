using System;
using System.Collections.Generic;

namespace WebApi.Models.Orders
{
    public class OrderModel
    {
        public Guid UserId { get; set; }
        public DateTime? Date { get; set; }
        public IEnumerable<Guid> ProductIds { get; set; }
    }
}