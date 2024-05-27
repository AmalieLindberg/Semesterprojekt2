using System.ComponentModel.DataAnnotations;

namespace Semesterprojekt2.Models.Shop
{
	public class Product
    {
        [Key]
		public int Id { get; set; }

		// [Display(Name = "Name")] - Annoteringen bruges til at vise et label i brugergrænsefladen.
		[Display(Name = "Name")]
		// [Required(ErrorMessage = "The product must be given a name")] - Gør feltet obligatorisk og angiver en fejlbesked.
		[Required(ErrorMessage = "The product must be given a name")]
        public string? Name { get; set; }

        [Display(Name = "Price")]
        [Required(ErrorMessage = "The product must be given a price")]
        public decimal? Price { get; set; }

        [Display(Name = "Type")]
        [Required(ErrorMessage = "The product must be given a type")]
        public string? Type { get; set; }

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
    
        public string? ProductImage { get; set; }

		

		// En konstruktør der initialiserer et nyt produkt med specifikke værdier for hver property.
		public Product(string name, decimal price, string type, string brand, int amount, string description)
        {
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
