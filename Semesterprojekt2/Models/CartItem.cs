using Semesterprojekt2.Models.Shop;
using System.ComponentModel.DataAnnotations;

namespace Semesterprojekt2.Models
{
    public class CartItem
    {
        // Antal af et produkt i indkøbskurven
        public int Quantity { get; set; }

        public Product Product { get; set; }

        // Produkt ID for det produkt, der er i indkøbskurven (påkrævet)
        [Required]
        public int ProductId { get; set; }


        public CartItem(Product product, int quantity)
        {
            Product = product;
            Quantity = quantity;
        }

        public CartItem()
        {
        }
    }
}
