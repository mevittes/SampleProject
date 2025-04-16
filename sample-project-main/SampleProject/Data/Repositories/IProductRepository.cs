using System;
using System.Collections.Generic;
using BusinessEntities;

namespace Data.Repositories
{
    public interface IProductRepository
    {
        IEnumerable<Product> Get(string name = null, decimal? price = null, string description = null);
        void DeleteAll();
        void Save(Product product);
        void Delete(Product product);
        Product Get(Guid id);
    }
}