using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Common.Extensions;

namespace BusinessEntities
{
    public class Order : IdObject
    {
        private readonly List<Guid> _productIds = new List<Guid>();
        private DateTime _date;
        private Guid _userId;

        public DateTime Date
        {
            get => _date;
            private set => _date = value;
        }

        public Guid UserId
        {
            get => _userId;
            private set => _userId = value;
        }

        public IEnumerable<Guid> ProductIds
        {
            get => _productIds;
            private set => _productIds.Initialize(value);
        }

        public void SetDate(DateTime? date)
        {
            if (!date.HasValue)
            {
                throw new ArgumentNullException("Date was not provided.");
            }
            _date = date.Value;
        }

        public void SetUserId(Guid userId)
        {
            _userId = userId;
        }

        public void SetProductIds(IEnumerable<Guid> productIds)
        {
            _productIds.Initialize(productIds);
        }
    }
}
