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
            // Finder eksisterende vare i indkøbskurven baseret på produktets ID
            var existingCartItem = _cartItems.FirstOrDefault(ci => ci.Product.Id == product.Id);
            if (existingCartItem != null)
            {
                // Hvis varen allerede findes i kurven, øges mængden
                existingCartItem.Quantity += quantity;
            }
            else
            {
                // Hvis varen ikke findes i kurven, tilføjes den med den angivne mængde
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
                if (cartItem.Product.Id == productId) // Adgang til Id fra Product objektet
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
            // Finder varen, der skal slettes, baseret på produktets ID
            CartItem cartItemToBeDeleted = _cartItems.FirstOrDefault(ci => ci.Product.Id == productId);
            if (cartItemToBeDeleted != null)
            {
                // Hvis mængden er større end 1, reduceres mængden
                if (cartItemToBeDeleted.Quantity > 1)
                {
                    cartItemToBeDeleted.Quantity -= 1;
                }
                else
                {
                    // Hvis mængden er 1 eller mindre, fjernes varen helt fra kurven
                    _cartItems.Remove(cartItemToBeDeleted);
                }
                return true; 
            }
            return false; 
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
                    total += (decimal)cartItem.Product.Price * cartItem.Quantity;
                }
                else
                {
                    // Håndter tilfældet, hvis prisen er null 
                    total += 0;
                }
            }
            return total;
        }

    }
}
