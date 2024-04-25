using Semesterprojekt2.Models;

namespace Semesterprojekt2.Service
{
    public interface IProductService
    {
        List<Product> GetProducts();
        void AddProduct(Product product);
    }
}
