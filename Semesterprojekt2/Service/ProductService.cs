using Semesterprojekt2.MockData.Shop;
using Semesterprojekt2.Models;
using Semesterprojekt2.Models.Shop;
using System.Runtime.CompilerServices;

namespace Semesterprojekt2.Service
{
    public class ProductService : IProductService
    {
        private List<Product> _products {  get; set; }
        private DbGenericService<Product> _dbGenericService {  get; set; }

        private JsonFileProductService JsonFileProductService { get; set; }

        public ProductService(JsonFileProductService jsonFileProductService, DbGenericService<Product> dbGenericService)
        {
            _dbGenericService = dbGenericService;
            JsonFileProductService = jsonFileProductService;
            //_products = MockProducts.GetMockProducts();
            //_products = JsonFileProductService.GetJsonProducts().ToList();
            //_dbGenericService.SaveObjects(_products);
            _products = _dbGenericService.GetObjectsAsync().Result.ToList();
        }

        public async Task AddProductAsync(Product product)
        {
            _products.Add(product);
          await _dbGenericService.AddObjectAsync(product);
        }

        public Product GetProduct(int productId)
        {
            foreach (Product product in _products)
            {
                if (product.Id == productId)
                    return product;
            }
            return null;
        }

        public async Task UpdateProductAsync(Product product)
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
                        if(product.ProductImage == null)
                        {
                            product.ProductImage = p.ProductImage;
                        }
                        p.ProductImage = product.ProductImage;
                    }
                }
                //JsonFileProductService.SaveJsonProducts(_products);
				await _dbGenericService.UpdateObjectAsync(product);
			}
        }

        public async Task<Product> DeleteProductAsync(int? productId)
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
                //JsonFileProductService.SaveJsonProducts(_products);
                await _dbGenericService.DeleteObjectAsync(productToBeDeleted);
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
