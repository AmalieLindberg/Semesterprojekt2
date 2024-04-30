using System.ComponentModel.DataAnnotations;

namespace Semesterprojekt2.Models.Shop
{
    public class Product
    {
        // En statisk variabel til at holde styr på det næste unikke ID for et produkt.
        private static int nextId = 1;
		// En property til at repræsentere produktets ID.
		public int Id { get; set; }

		// [Display(Name = "Name")] - Annoteringen bruges til at vise et label i brugergrænsefladen.
		[Display(Name = "Name")]
		// [Required(ErrorMessage = "The product must be given a name")] - Gør feltet obligatorisk og angiver en fejlbesked.
		[Required(ErrorMessage = "The product must be given a name")]
        public string? Name { get; set; }

        [Display(Name = "Price")]
        [Required(ErrorMessage = "The product must be given a price")]
        public double? Price { get; set; }

        [Display(Name = "Type")]
        [Required(ErrorMessage = "The product must be given a type")]
        public ProductType? Type { get; set; }

        [Display(Name = "Brand")]
        [Required(ErrorMessage = "The product must be given a brand")]
        public string? Brand { get; set; }

        [Display(Name = "Amount")]
        [Required(ErrorMessage = "The product must be given an amount")]
        public int? Amount { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "The product must be given a description")]
        public string? Description { get; set; }

        [Display(Name = "Image")]
        [Required(ErrorMessage = "The product must be given an image")]
        public string? ProductImage { get; set; }

		// En enumeration der definerer de forskellige typer af produkter som kan eksistere.
		public enum ProductType
        {
            Clothing,
            Soap,
            Treats,
            Toy
        }

		// En konstruktør der initialiserer et nyt produkt med specifikke værdier for hver egenskab.
		public Product(string name, double price, ProductType type, string brand, int amount, string description)
        {
            Id = nextId++; // Tildeler et unikt ID til produktet og inkrementerer det næste ID.
			Name = name;
            Price = price;
            Type = type;
            Brand = brand;
            Amount = amount;
            Description = description;
        }

		// En standard konstruktør uden parametre. Dette er nyttigt, hvis man opretter et produkt uden straks at kende alle detaljerne.
		public Product()
        {
        }
    }
}
