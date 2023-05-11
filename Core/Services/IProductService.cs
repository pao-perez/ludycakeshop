using LudyCakeShop.Core.Domain.Data;
using System.Collections.Generic;

namespace LudyCakeShop.Core.Services
{
    public interface IProductService
    {
        public string CreateProduct(Product product);
        public bool UpdateProduct(string productID, Product product);
        public Product GetProduct(string productID);
        public IEnumerable<Product> GetProducts();
        public bool DeleteProduct(string productID);
    }
}
