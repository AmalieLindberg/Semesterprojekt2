using Semesterprojekt2.Models;
using Semesterprojekt2.Models.Shop;
using Semesterprojekt2.Service.BookATimeService;
using System.Text.Json;

namespace Semesterprojekt2.Service
{
    public class ShoppingCartService
    {
       //private DbGenericService<CartItem> _dbGenericService {  get; set; }
        private List<CartItem> _cartItems;

        public ShoppingCartService(/*DbGenericService<CartItem> dbGenericService*/)
        { 
            //_dbGenericService = dbGenericService;
            _cartItems = new List<CartItem>();
            //_cartItems = _dbGenericService.GetObjectsAsync().Result.ToList();
        }

        public void AddToCart(Product product, int quantity)
        {
            var existingCartItem = _cartItems.FirstOrDefault(ci => ci.Product.Id == product.Id);
            if (existingCartItem != null)
            {
                // If the item already exists in the cart, increase its quantity
                existingCartItem.Quantity += quantity;
            }
            else
            {
                // If the item is not in the cart, add it with the specified quantity
                _cartItems.Add(new CartItem
                {
                    Product = product,
                    Quantity = quantity,
                });
            }
        }

        public CartItem GetCartItem(int productId)
        {
            foreach (CartItem cartItem in _cartItems)
            {
                if (cartItem.Product.Id == productId) // Access Id from Product object
                {
                    return cartItem;
                }
            }
            return null;
        }

        public List<CartItem> GetAllCartItems()
        {
            return _cartItems;
        }

        public bool DeleteCartItem(int productId)
        {
            CartItem cartItemToBeDeleted = _cartItems.FirstOrDefault(ci => ci.Product.Id == productId);
            if (cartItemToBeDeleted != null)
            {
                if (cartItemToBeDeleted.Quantity > 1)
                {
                    // Reduce the quantity by one if more than one exists
                    cartItemToBeDeleted.Quantity -= 1;
                }
                else
                {
                    // Remove the item completely if only one is left
                    _cartItems.Remove(cartItemToBeDeleted);
                }
                return true; // Indicates successful deletion or decrement
            }
            return false; // Indicates the item was not found and nothing was deleted
        }

        public void ClearCart()
        {
            _cartItems.Clear();
        }

        public decimal CalculateTotalPrice()
        {
            decimal total = 0m;
            foreach (var cartItem in _cartItems)
            {
                if (cartItem.Product.Price != null)
                {
                    // Safely cast double to decimal and calculate total
                    total += (decimal)cartItem.Product.Price * cartItem.Quantity;
                }
                else
                {
                    // Handle the case where the price is null if needed
                    // For example, you could assume a price of 0, or log an error, etc.
                    // total += 0; // Uncomment this if you want to add 0 to the total when the price is null
                }
            }
            return total;
        }

    }
}
