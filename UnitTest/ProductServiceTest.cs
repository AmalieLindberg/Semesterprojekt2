using Semesterprojekt2.Models.Shop;
using Semesterprojekt2.Service;

namespace UnitTest
{
    [TestClass]
    public class ProductServiceTest
    {
       
        private ProductService _productService;
        private List<Product> _products;

        [TestInitialize]
        public void BeforeTest()
        {
            _products = new List<Product>();
       


            // Initialize ProductService with mocked dependencies
            _productService = new ProductService(_products);
        }

        [TestMethod]
        public void AddProduct_ShouldAddProductToList()
        {
            // Arrange
            var product = new Product 
            { Id = 1, Type = "Test Product", Amount = 121, 
                Brand = "Test Product", Name = "Test Product", 
                Description = "Test Product", Price = 121, ProductImage = "Test Product" };

            // Act
            _productService.AddProductAsync(product);

            // Assert
            Assert.AreEqual(1, _products.Count);
            Assert.AreEqual(product, _products[0]);
           
        }

        [TestMethod]
        public void DeleteProduct_ShouldDeleteProductFromList()
        {
            // Arrange
            var product = new Product { Id = 1, Type = "Test Product", 
                Amount = 121, Brand = "Test Product", Name = "Test Product", 
                Description = "Test Product", Price = 121, ProductImage = "Test Product" };
            var product1 = new Product
            {
                Id = 2,  Type = "Test Product", Amount = 121,  Brand = "Test Product",
                Name = "Test Product", Description = "Test Product", Price = 121, ProductImage = "Test Product"
            };
            _productService.AddProductAsync(product);
            _productService.AddProductAsync(product1);

            // Act

            _productService.DeleteProductAsync(1);

            // Assert
            Assert.AreEqual(1, _products.Count);
            Assert.AreEqual(product1, _products[0]);
        }
         [TestMethod]
        public void UpdateProduct_ShouldUpdateProductInList()
        {
            // Arrange
            var originalProduct = new Product { Id = 1, Type = "Test Product", 
                Amount = 121, Brand = "Test Product", Name = "Test Product", 
                Description = "Test Product", Price = 121, ProductImage = "OriginalImage.jpg" };
            _productService.AddProductAsync(originalProduct);
            var updatedProduct = new Product
            {
                Id = 1, Name = "Updated Name", Price = 150,Type = "Updated Type", 
                Brand = "Updated Brand", Amount = 20,  Description = "Updated Description", ProductImage = null // billede skulle blive det samme
            };
            //Act
            _productService.UpdateProductAsync(updatedProduct);

            //Assert

            var product = _products.FirstOrDefault(p => p.Id == 1);
            Assert.IsNotNull(product);
            Assert.AreEqual("Updated Name", product.Name);
            Assert.AreEqual(150, product.Price);
            Assert.AreEqual("Updated Type", product.Type);
            Assert.AreEqual("Updated Brand", product.Brand);
            Assert.AreEqual(20, product.Amount);
            Assert.AreEqual("Updated Description", product.Description);
            Assert.AreEqual("OriginalImage.jpg", product.ProductImage);
        }

        [TestMethod]
        public void GetProducts_ShouldReturnAllProducts()
        {
            // Arrange
            var product1 = new Product
            {
                Id = 1, Name = "Product 1", Price = 100, Type = "Type 1", 
                Brand = "Brand 1", Amount = 10, Description = "Description 1", ProductImage = "Image1.jpg"
            };

            var product2 = new Product
            {
                Id = 2, Name = "Product 2",   Price = 200, Type = "Type 2",
                Brand = "Brand 2",   Amount = 20,Description = "Description 2", ProductImage = "Image2.jpg"
            };

            _products.Add(product1);
            _products.Add(product2);

            // Act
            var products = _productService.GetProducts();

            // Assert
            Assert.AreEqual(2, products.Count);
            Assert.AreEqual(product1, products[0]);
            Assert.AreEqual(product2, products[1]);
        }
        [TestMethod]
        public void GetProduct_ShouldReturnProductById()
        {// Arrange
            var product1 = new Product
            {
                Id = 1,
                Name = "Product 1",
                Price = 100,
                Type = "Type 1",
                Brand = "Brand 1",
                Amount = 10,
                Description = "Description 1",
                ProductImage = "Image1.jpg"
            };

            _products.Add(product1);
            // Act
            var retrievedProduct = _productService.GetProduct(1);

            // Assert
            Assert.IsNotNull(retrievedProduct);


        }
        [TestMethod]
        public void GetProduct_ShouldReturnNullIfProductNotFound()
        {
            // Arrange
            var product1 = new Product
            {
                Id = 1,
                Name = "Product 1",
                Price = 100,
                Type = "Type 1",
                Brand = "Brand 1",
                Amount = 10,
                Description = "Description 1",
                ProductImage = "Image1.jpg"
            };

            _products.Add(product1);

            // Act
            var retrievedProduct = _productService.GetProduct(2);

            // Assert
            Assert.IsNull(retrievedProduct);
        }
        [TestMethod]
     
        public void NameSearch_ShouldReturnMatchingProducts()
        {
            // Arrange
            var product1 = new Product
            {
                Id = 1,
                Name = "Apple",
                Price = 100,
                Type = "Fruit",
                Brand = "Brand 1",
                Amount = 10,
                Description = "Description 1",
                ProductImage = "Image1.jpg"
            };

            var product2 = new Product
            {
                Id = 2,
                Name = "Banana",
                Price = 200,
                Type = "Fruit",
                Brand = "Brand 2",
                Amount = 20,
                Description = "Description 2",
                ProductImage = "Image2.jpg"
            };

            var product3 = new Product
            {
                Id = 3,
                Name = "Carrot",
                Price = 150,
                Type = "Vegetable",
                Brand = "Brand 3",
                Amount = 15,
                Description = "Description 3",
                ProductImage = "Image3.jpg"
            };

            _products.Add(product1);
            _products.Add(product2);
            _products.Add(product3);

            // Act
            var result = _productService.NameSearch("App");

            // Assert
            var resultList = result.ToList();
            Assert.AreEqual(1, resultList.Count);
            Assert.IsTrue(resultList.Contains(product1));
           
        }

        [TestMethod]
        public void NameSearch_ShouldReturnAllProductsWhenSearchStringIsEmpty()
        {
            // Arrange
            var product1 = new Product
            {
                Id = 1,
                Name = "Apple",
                Price = 100,
                Type = "Fruit",
                Brand = "Brand 1",
                Amount = 10,
                Description = "Description 1",
                ProductImage = "Image1.jpg"
            };

            var product2 = new Product
            {
                Id = 2,
                Name = "Banana",
                Price = 200,
                Type = "Fruit",
                Brand = "Brand 2",
                Amount = 20,
                Description = "Description 2",
                ProductImage = "Image2.jpg"
            };

            _products.Add(product1);
            _products.Add(product2);

            // Act
            var result = _productService.NameSearch("");

            // Assert
            var resultList = result.ToList();
            Assert.AreEqual(2, resultList.Count);
            Assert.IsTrue(resultList.Contains(product1));
            Assert.IsTrue(resultList.Contains(product2));
        }
        [TestMethod]
        public void NameSearch_ShouldReturnEmptyWhenNoProductsMatch()
        {
            // Arrange
            var product1 = new Product
            {
                Id = 1,
                Name = "Apple",
                Price = 100,
                Type = "Fruit",
                Brand = "Brand 1",
                Amount = 10,
                Description = "Description 1",
                ProductImage = "Image1.jpg"
            };

            var product2 = new Product
            {
                Id = 2,
                Name = "Banana",
                Price = 200,
                Type = "Fruit",
                Brand = "Brand 2",
                Amount = 20,
                Description = "Description 2",
                ProductImage = "Image2.jpg"
            };

            _products.Add(product1);
            _products.Add(product2);

            // Act
            var result = _productService.NameSearch("xyz");

            // Assert
            var resultList = result.ToList();
            Assert.AreEqual(0, resultList.Count);
        }
        [TestMethod]
        public void PriceFilter_ShouldReturnProductsWithinPriceRange()
        {
            // Arrange
            var product1 = new Product
            {
                Id = 1,
                Name = "Product 1",
                Price = 50,
                Type = "Type 1",
                Brand = "Brand 1",
                Amount = 10,
                Description = "Description 1",
                ProductImage = "Image1.jpg"
            };

            var product2 = new Product
            {
                Id = 2,
                Name = "Product 2",
                Price = 150,
                Type = "Type 2",
                Brand = "Brand 2",
                Amount = 20,
                Description = "Description 2",
                ProductImage = "Image2.jpg"
            };

            var product3 = new Product
            {
                Id = 3,
                Name = "Product 3",
                Price = 300,
                Type = "Type 3",
                Brand = "Brand 3",
                Amount = 30,
                Description = "Description 3",
                ProductImage = "Image3.jpg"
            };

            _products.Add(product1);
            _products.Add(product2);
            _products.Add(product3);

            // Act
            var result = _productService.PriceFilter(200, 100);

            // Assert
            var resultList = result.ToList();
            Assert.AreEqual(1, resultList.Count);
            Assert.IsTrue(resultList.Contains(product2));
        }

        [TestMethod]
        public void PriceFilter_ShouldReturnProductsBelowMaxPrice()
        {
            // Arrange
            var product1 = new Product
            {
                Id = 1,
                Name = "Product 1",
                Price = 50,
                Type = "Type 1",
                Brand = "Brand 1",
                Amount = 10,
                Description = "Description 1",
                ProductImage = "Image1.jpg"
            };

            var product2 = new Product
            {
                Id = 2,
                Name = "Product 2",
                Price = 150,
                Type = "Type 2",
                Brand = "Brand 2",
                Amount = 20,
                Description = "Description 2",
                ProductImage = "Image2.jpg"
            };

            var product3 = new Product
            {
                Id = 3,
                Name = "Product 3",
                Price = 300,
                Type = "Type 3",
                Brand = "Brand 3",
                Amount = 30,
                Description = "Description 3",
                ProductImage = "Image3.jpg"
            };

            _products.Add(product1);
            _products.Add(product2);
            _products.Add(product3);

            // Act
            var result = _productService.PriceFilter(200);

            // Assert
            var resultList = result.ToList();
            Assert.AreEqual(2, resultList.Count);
            Assert.IsTrue(resultList.Contains(product1));
            Assert.IsTrue(resultList.Contains(product2));
        }

        [TestMethod]
        public void PriceFilter_ShouldReturnProductsAboveMinPrice()
        {
            // Arrange
            var product1 = new Product
            {
                Id = 1,
                Name = "Product 1",
                Price = 50,
                Type = "Type 1",
                Brand = "Brand 1",
                Amount = 10,
                Description = "Description 1",
                ProductImage = "Image1.jpg"
            };

            var product2 = new Product
            {
                Id = 2,
                Name = "Product 2",
                Price = 150,
                Type = "Type 2",
                Brand = "Brand 2",
                Amount = 20,
                Description = "Description 2",
                ProductImage = "Image2.jpg"
            };

            var product3 = new Product
            {
                Id = 3,
                Name = "Product 3",
                Price = 300,
                Type = "Type 3",
                Brand = "Brand 3",
                Amount = 30,
                Description = "Description 3",
                ProductImage = "Image3.jpg"
            };

            _products.Add(product1);
            _products.Add(product2);
            _products.Add(product3);

            // Act
            var result = _productService.PriceFilter(0, 100);

            // Assert
            var resultList = result.ToList();
            Assert.AreEqual(2, resultList.Count);
            Assert.IsTrue(resultList.Contains(product2));
            Assert.IsTrue(resultList.Contains(product3));
        }

        [TestMethod]
        public void PriceFilter_ShouldReturnAllProductsWhenDefaultValues()
        {
            // Arrange
            var product1 = new Product
            {
                Id = 1,
                Name = "Product 1",
                Price = 50,
                Type = "Type 1",
                Brand = "Brand 1",
                Amount = 10,
                Description = "Description 1",
                ProductImage = "Image1.jpg"
            };

            var product2 = new Product
            {
                Id = 2,
                Name = "Product 2",
                Price = 150,
                Type = "Type 2",
                Brand = "Brand 2",
                Amount = 20,
                Description = "Description 2",
                ProductImage = "Image2.jpg"
            };

            var product3 = new Product
            {
                Id = 3,
                Name = "Product 3",
                Price = 300,
                Type = "Type 3",
                Brand = "Brand 3",
                Amount = 30,
                Description = "Description 3",
                ProductImage = "Image3.jpg"
            };

            _products.Add(product1);
            _products.Add(product2);
            _products.Add(product3);

            // Act
            var result = _productService.PriceFilter(0);

            // Assert
            var resultList = result.ToList();
            Assert.AreEqual(3, resultList.Count);
            Assert.IsTrue(resultList.Contains(product1));
            Assert.IsTrue(resultList.Contains(product2));
            Assert.IsTrue(resultList.Contains(product3));
        }

    }
}
