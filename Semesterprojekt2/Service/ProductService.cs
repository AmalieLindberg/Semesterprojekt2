using Semesterprojekt2.MockData.Shop;
using Semesterprojekt2.Models;
namespace Semesterprojekt2.Service
{
    public class ProductService : IProductService
    {
        private List<Product> _products;

        public ProductService()
        {
            _products = MockProducts.GetMockProducts();
        }

        public void AddProduct(Product product)
        {
            _products.Add(product);
        }

        public List<Product> GetProducts() { return _products; }
    }
}
