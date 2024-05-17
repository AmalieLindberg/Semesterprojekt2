using Semesterprojekt2.Models.Shop;
using System.ComponentModel.DataAnnotations;

namespace Semesterprojekt2.Models
{
    public class CartItem
    {
       
        public int Quantity { get; set; }
        public Product Product { get; set; }
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
