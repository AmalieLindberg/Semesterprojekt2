using Semesterprojekt2.Models.Shop;

namespace Semesterprojekt2.Service
{
    public interface IProductService
    {
        List<Product> GetProducts();
        void AddProduct(Product product);
        void UpdateProduct(Product product);
        Product GetProduct(int id);
        Product DeleteProduct(int? productId);
        IEnumerable<Product> NameSearch(string str);
		IEnumerable<Product> PriceFilter(int maxPrice, int minPrice = 0);
	}
}
