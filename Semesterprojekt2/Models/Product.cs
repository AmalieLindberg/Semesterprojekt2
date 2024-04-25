namespace Semesterprojekt2.Models
{
    public class Product
    {
        private static int nextId;
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public ProductType Type { get; set; }
        public string Brand { get; set; }
        public int Amount { get; set; }
        public string Description { get; set; }

        public enum ProductType
        {
            Clothing,
            Soap,
            Treats,
            Toy
        }

        public Product(string name, double price, ProductType type, string brand, int amount, string description)
        {
            Id = nextId++;
            Name = name;
            Price = price;
            Type = type;
            Brand = brand;
            Amount = amount;
            Description = description;
        }

        public Product()
        {
        }
    }
}
