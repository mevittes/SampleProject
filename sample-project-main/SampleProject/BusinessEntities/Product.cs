using System;

namespace BusinessEntities
{
    public class Product : IdObject
    {
        private decimal _price;
        private string _name;
        private string _description;

        public decimal Price
        {
            get => _price;
            private set => _price = value;
        }

        public string Name
        {
            get => _name;
            private set => _name = value;
        }

        public string Description
        {
            get => _description;
            private set => _description = value;
        }

        public void SetName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("Name was not provided.");
            }
            _name = name;
        }

        public void SetPrice(decimal? price)
        {
            if (!price.HasValue)
            {
                throw new ArgumentNullException("Price was not provided.");
            }
            _price = price.Value;
        }

        public void SetDescription(string description)
        {
            _description = description;
        }
    }
}
