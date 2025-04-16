using BusinessEntities;

namespace WebApi.Models.Products
{
    public class ProductData : IdObjectData
    {
        public ProductData(Product product) : base(product)
        {
            Name = product.Name;
            Price = product.Price;
            Description = product.Description;
        }

        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
    }
}