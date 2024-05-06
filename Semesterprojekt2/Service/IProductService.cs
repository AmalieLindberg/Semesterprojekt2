using Semesterprojekt2.Models.Shop;

namespace Semesterprojekt2.Service
{
    public interface IProductService
    {
        List<Product> GetProducts();
        Task AddProductAsync(Product product);
        Task UpdateProductAsync(Product product);
        Product GetProduct(int productId);
        Product DeleteProduct(int? productId);
        IEnumerable<Product> NameSearch(string str);
		IEnumerable<Product> PriceFilter(int maxPrice, int minPrice = 0);
	}
}
