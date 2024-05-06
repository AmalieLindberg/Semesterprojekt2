using Semesterprojekt2.Models.Shop;
using System.ComponentModel.DataAnnotations;

namespace Semesterprojekt2.Models
{
    public class CartItem
    {
        public int CartId { get; set; }
        public int Quantity { get; set; }
        public Product Product { get; set; }

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
