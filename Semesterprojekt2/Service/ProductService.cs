using Semesterprojekt2.MockData.Shop;
using Semesterprojekt2.Models.Shop;

namespace Semesterprojekt2.Service
{
    public class ProductService : IProductService
    {
        private List<Product> _products;

        private JsonFileProductService JsonFileProductService { get; set; }

        public ProductService(JsonFileProductService jsonFileProductService)
        {
            JsonFileProductService = jsonFileProductService;
            //_products = MockProducts.GetMockProducts();
            _products = JsonFileProductService.GetJsonProducts().ToList();
        }

        public ProductService()
        {
            _products = MockProducts.GetMockProducts();
        }

        public void AddProduct(Product product)
        {
            _products.Add(product);
            JsonFileProductService.SaveJsonProducts(_products);
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
                        p.Type = product.Type;
                        p.Brand = product.Brand;
                        p.Amount = product.Amount;
                        p.Description = product.Description;
                    }
                }
                JsonFileProductService.SaveJsonProducts(_products);
            }
        }

        public Product DeleteProduct(int? productId)
        {
            Product productToBeDeleted = null;
            foreach (Product product in _products)
            {
                if (product.Id == productId)
                {
                    productToBeDeleted = product;
                    break;
                }
            }

            if (productToBeDeleted != null)
            {
                _products.Remove(productToBeDeleted);
                JsonFileProductService.SaveJsonProducts(_products);
            }

            return productToBeDeleted;
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
