using System;
using System.Collections.Generic;
using BusinessEntities;

namespace Core.Services.Products
{
    public interface IGetProductService
    {
        Product GetProduct(Guid id);

        IEnumerable<Product> GetProducts(string name = null, decimal? price = null, string description = null);
    }
}