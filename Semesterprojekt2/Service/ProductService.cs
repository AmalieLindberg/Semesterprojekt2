using Semesterprojekt2.MockData.Shop;
using Semesterprojekt2.Models.Shop;
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

        public Product GetProduct(int id)
        {
            foreach (Product product in _products)
            {
                if (product.Id == id)
                    return product;
            }

            return null;
        }

        public void UpdateProduct(Product product)
        {
            if (product != null)
            {
                foreach (Product p in _products)
                {
                    if (p.Id == product.Id)
                    {
                        p.Name = product.Name;
                        p.Price = product.Price;
                    }
                }
            }
        }

        public Product DeleteProduct(int? productId)
        {
            foreach (Product product in _products)
            {
                if (product.Id == productId)
                {
                    _products.Remove(product);
                    return product;
                }
            }
            return null;
        }

        public List<Product> GetProducts() { return _products; }

		public IEnumerable<Product> NameSearch(string str)
		{
			List<Product> nameSearch = new List<Product>();
			foreach (Product product in _products)
			{
				if (string.IsNullOrEmpty(str) || product.Name.ToLower().Contains(str.ToLower()))
				{
					nameSearch.Add(product);
				}
			}

			return nameSearch;
		}

		public IEnumerable<Product> PriceFilter(int maxPrice, int minPrice = 0)
		{
			List<Product> filterList = new List<Product>();
			foreach (Product product in _products)
			{
				if ((minPrice == 0 && product.Price <= maxPrice) || (maxPrice == 0 && product.Price >= minPrice) || (product.Price >= minPrice && product.Price <= maxPrice))
				{
					filterList.Add(product);
				}
			}

			return filterList;
		}
	}
}
