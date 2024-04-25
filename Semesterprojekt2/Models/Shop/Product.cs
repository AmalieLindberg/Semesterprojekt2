using System.ComponentModel.DataAnnotations;

namespace Semesterprojekt2.Models.Shop
{
    public class Product
    {
        private static int nextId = 1;
        public int Id { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "The product must be given a name")]
        public string Name { get; set; }

        [Display(Name = "Price")]
        [Required(ErrorMessage = "The product must be given a price")]
        public double? Price { get; set; }

        [Display(Name = "Type")]
        [Required(ErrorMessage = "The product must be given a type")]
        public ProductType Type { get; set; }

        [Display(Name = "Brand")]
        [Required(ErrorMessage = "The product must be given a brand")]
        public string Brand { get; set; }

        [Display(Name = "Amount")]
        [Required(ErrorMessage = "The product must be given an amount")]
        public int? Amount { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "The product must be given a description")]
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
