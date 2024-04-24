namespace Semesterprojekt2.Models.Shop
{
    public class Product
    {
        private static int nextId;
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public Type Type { get; set; }
        public string Brand { get; set; }
        public int Amount { get; set; }
        public string Description { get; set; }

        public Product(string name, double price, Type type, string brand, int amount, string description)
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
