using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Semesterprojekt2.Models;
using Semesterprojekt2.Models.Shop;
using Semesterprojekt2.Service;

namespace Semesterprojekt2.Pages.ShoppingCart
{
    public class ShoppingCartModel : PageModel
    {
        private ShoppingCartService _shoppingCartService;

        public ShoppingCartModel(ShoppingCartService shoppingCartService)
        {
            _shoppingCartService = shoppingCartService;
        }

        public List<CartItem> CartItems { get; set; } = new List<CartItem>();

        public List<Models.Shop.Product>? Products { get; private set; }

        public void OnGet()
        {
            CartItems = _shoppingCartService.GetAllCartItems(); 
        }

        public IActionResult OnPostRemoveSingleCartItem(int productId)
        {
            _shoppingCartService.DeleteCartItem(productId);
            return RedirectToPage();
        }

        public IActionResult OnPostRemoveAllCartItems()
        {
            _shoppingCartService.ClearCart();
            return RedirectToPage();
        }

        public decimal TotalPrice
        {
            get
            {
                return CartItems.Sum(item => (item.Product.Price ?? 0) * item.Quantity);
            }
        }
    }
}
