using System;
using System.Collections.Generic;
using System.Linq;
using BusinessEntities;
using Common;
using Microsoft.Extensions.Caching.Memory;

namespace Data.Repositories
{
    [AutoRegister]
    public class ProductRepository : IProductRepository
    {
        private readonly IInternalCache _memoryCache;

        private const string _cacheKeyForProduct = "ProductsDictionary";

        public ProductRepository(IInternalCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public IEnumerable<Product> Get(string name = null, decimal? price = null, string description = null)
        {
            IEnumerable<Product> products = GetProductData().Values;
            if (name != null)
            {
                products = products.Where(o => o.Name == name);
            }
            if (price != null)
            {
                products = products.Where(o => o.Price == price);
            }
            if (description != null)
            {
                products = products.Where(o => o.Description.Contains(description));
            }
            return products.ToList();
        }

        private IDictionary<Guid, Product> GetProductData()
        {
            if (_memoryCache.TryGetValue(_cacheKeyForProduct, out var products))
            {
                return (IDictionary<Guid, Product>)products;
            }
            var newProducts = new Dictionary<Guid, Product>();
            _memoryCache.Set<IDictionary<Guid, Product>>(_cacheKeyForProduct, newProducts);
            return newProducts;
        }

        public void DeleteAll()
        {
            var products = new Dictionary<Guid, Product>();
            _memoryCache.Set(_cacheKeyForProduct, products);
        }

        public void Save(Product product)
        {
            var products = GetProductData();
            products[product.Id] = product;
            _memoryCache.Set(_cacheKeyForProduct, products);
        }

        public void Delete(Product product)
        {
            var products = GetProductData();
            products.Remove(product.Id);
            _memoryCache.Set(_cacheKeyForProduct, products);
        }

        public Product Get(Guid id)
        {
            var products = GetProductData();
            return products.ContainsKey(id) ? products[id] : null;
        }
    }
}