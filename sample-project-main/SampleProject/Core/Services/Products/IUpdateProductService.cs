using System.Collections.Generic;
using BusinessEntities;

namespace Core.Services.Products
{
    public interface IUpdateProductService
    {
        void Update(Product product, string name, decimal? price, string description);
        IEnumerable<string> ValidateModelInputs(string name, decimal? price, string description);
    }
}