using Semesterprojekt2.Models.Shop;
using Semesterprojekt2.Service;

namespace UnitTest
{
    [TestClass]
    public class ProductServiceTest
    {
       
        private ProductService _productService;
        private List<Product> _products;
        //Bliver kaldt før være test.
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
            //Laver et nyt object
            var product = new Product 
            { Id = 1, Type = "Test Product", Amount = 121, 
                Brand = "Test Product", Name = "Test Product", 
                Description = "Test Product", Price = 121, ProductImage = "Test Product" };
           
            // Act 
            //Tilføjer til liste
            _productService.AddProductAsync(product);

            // Assert
            //Checker om listen indholder 1 element
            //Og ser om det første element er det objekt som vi har lavet i starten
            Assert.AreEqual(1, _products.Count);
            Assert.AreEqual(product, _products[0]);
           
        }

        [TestMethod]
        public void DeleteProduct_ShouldDeleteProductFromList()
        {
            // Arrange
            //Laver to obejkter og tilføjer begge til en liste
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
            //Sletter obejktet med id 1.
            _productService.DeleteProductAsync(1);

            // Assert
            //Checker om der kun er et element i listen
            //Og om det med id 2
            Assert.AreEqual(1, _products.Count);
            Assert.AreEqual(product1, _products[0]);
        }
         [TestMethod]
        public void UpdateProduct_ShouldUpdateProductInList()
        {
            // Arrange
            //Laver to obejkter (De har begge id 1)
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
            //Updater obejektet
            _productService.UpdateProductAsync(updatedProduct);

            //Assert
            //Checker om tingene passer
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
            //Laver to obejkter og tilføjer dem til listen.
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
            //Henter alle produkterne
            var products = _productService.GetProducts();

            // Assert
            //Checker om der er to elementer i listen
            //Ser om både objekt 1 og 2 er i listen.
            Assert.AreEqual(2, products.Count);
            Assert.AreEqual(product1, products[0]);
            Assert.AreEqual(product2, products[1]);
        }
        [TestMethod]
        public void GetProduct_ShouldReturnProductById()
        {// Arrange
         //Laver et obejkt og tilføjer den til listen.
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
            //henter produkt med id 1.
            var retrievedProduct = _productService.GetProduct(1);

            // Assert
            //Checker om det er null
            Assert.IsNotNull(retrievedProduct);


        }
        [TestMethod]
        public void GetProduct_ShouldReturnNullIfProductNotFound()
        {
            // Arrange
            //Laver et obejkt og tilføjer den til listen.
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
            //Henter produkt med id 2
            var retrievedProduct = _productService.GetProduct(2);

            // Assert
            //Checker at det ikke er i listen fordi vi ikke har det.
            Assert.IsNull(retrievedProduct);
        }
        [TestMethod]
     
        public void NameSearch_ShouldReturnMatchingProducts()
        {
            // Arrange
            //Laver 3 obejkt og tilføjer dem til listen.
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
            //Søg med noget at navnet
            var result = _productService.NameSearch("App");

            // Assert
            //Ser om den henter noget som matcher lidt
            var resultList = result.ToList();
            Assert.AreEqual(1, resultList.Count);
            Assert.IsTrue(resultList.Contains(product1));
           
        }

        [TestMethod]
        public void NameSearch_ShouldReturnAllProductsWhenSearchStringIsEmpty()
        {
            // Arrange
            //Laver 2 obejkt og tilføjer den til listen.
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
            //Søg ud at skrive noget
            var result = _productService.NameSearch("");

            // Assert
            //Ser at den returner alle.
            var resultList = result.ToList();
            Assert.AreEqual(2, resultList.Count);
            Assert.IsTrue(resultList.Contains(product1));
            Assert.IsTrue(resultList.Contains(product2));
        }
        [TestMethod]
        public void NameSearch_ShouldReturnEmptyWhenNoProductsMatch()
        {
            // Arrange
            //Laver 2 obejkt og tilføjer den til listen.
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
            //Søger på noget som ikke findes
            var result = _productService.NameSearch("xyz");

            // Assert
            //Ser at den ikke matcher noget.
            var resultList = result.ToList();
            Assert.AreEqual(0, resultList.Count);
        }
        [TestMethod]
        public void PriceFilter_ShouldReturnProductsWithinPriceRange()
        {
            // Arrange
            //Laver 3 obejkt og tilføjer den til listen.
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
            //Filter mellem prisen
            var result = _productService.PriceFilter(200, 100);

            // Assert
            //Skal return alle dem i mellem som er 1.
            var resultList = result.ToList();
            Assert.AreEqual(1, resultList.Count);
            Assert.IsTrue(resultList.Contains(product2));
        }

        [TestMethod]
        public void PriceFilter_ShouldReturnProductsBelowMaxPrice()
        {
            // Arrange
            //Laver 3 obejkt og tilføjer den til listen.
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
            //filter max pris så den ikke er over 200
            var result = _productService.PriceFilter(200);

            // Assert
            //returner 2 som er under max pris.
            //Checker om den er i listen.
            var resultList = result.ToList();
            Assert.AreEqual(2, resultList.Count);
            Assert.IsTrue(resultList.Contains(product1));
            Assert.IsTrue(resultList.Contains(product2));
        }

        [TestMethod]
        public void PriceFilter_ShouldReturnProductsAboveMinPrice()
        {
            // Arrange
            //Laver 3 obejkt og tilføjer den til listen.
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
            //Filter at den kommer over min pris som er 100
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
            //Laver 3 obejkt og tilføjer den til listen.
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
            //Checker når den er tom
            var result = _productService.PriceFilter(0);

            // Assert
            //Viser hele listen.
            var resultList = result.ToList();
            Assert.AreEqual(3, resultList.Count);
            Assert.IsTrue(resultList.Contains(product1));
            Assert.IsTrue(resultList.Contains(product2));
            Assert.IsTrue(resultList.Contains(product3));
        }

    }
}
