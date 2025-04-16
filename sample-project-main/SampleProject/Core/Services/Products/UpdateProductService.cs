using System.Collections.Generic;
using BusinessEntities;
using Common;

namespace Core.Services.Products
{
    [AutoRegister(AutoRegisterTypes.Singleton)]
    public class UpdateProductService : IUpdateProductService
    {
        public void Update(Product product, string name, decimal? price, string description)
        {
            product.SetName(name);
            product.SetPrice(price);
            product.SetDescription(description);
        }

        public IEnumerable<string> ValidateModelInputs(string name, decimal? price, string description)
        {
            var validationErrors = new List<string>();
            if (string.IsNullOrEmpty(name))
            {
                validationErrors.Add("Name was not provided.");
            }
            if (!price.HasValue)
            {
                validationErrors.Add("Price was not provided.");
            }
            return validationErrors;
        }
    }
}