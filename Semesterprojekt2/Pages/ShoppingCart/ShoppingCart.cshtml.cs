using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Semesterprojekt2.DAO;
using Semesterprojekt2.Models;
using Semesterprojekt2.Models.Shop;
using Semesterprojekt2.Pages.Login;
using Semesterprojekt2.Service;
using Semesterprojekt2.Service.UserService.UserService;

namespace Semesterprojekt2.Pages.ShoppingCart
{
    public class ShoppingCartModel : PageModel
    {
        private ShoppingCartService _shoppingCartService;
        private IProductService _productService;
        private IUserService _userService;

        public ShoppingCartModel(ShoppingCartService shoppingCartService, IProductService productService, IUserService userService)
        {
            _shoppingCartService = shoppingCartService;
            _productService = productService;
            _userService = userService;
        }

        public List<Models.CartItem> CartItem { get; set; } = new List<CartItem>();
        public Models.Shop.Product Product { get; set; }
        public Models.CartItem CartItems { get; set; }
        public Users User { get; set; }
        public Models.Shop.ProductOrder Order { get; set; } = new Models.Shop.ProductOrder();

        [BindProperty]
        public int Count { get; set; }

        public void OnGet(int id)
        {
            id = LoginModel.LoggedInUser.UserId;
            Users CurrentUser = _userService.GetUser(id);
            CartItem = _shoppingCartService.GetAllCartItems();
            Product = _productService.GetProduct(id);
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
                return CartItem.Sum(item => (item.Product.Price ?? 0) * item.Quantity);
            }
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            int id = LoginModel.LoggedInUser.UserId;
            Product = _productService.GetProduct(id);
            User = _userService.GetUser(id);
            Order.UserId = User.UserId;
            Order.ProductId = Product.Id;
            Order.Count = Count;
            _shoppingCartService.AddToCart(Product,Count);
            return RedirectToPage("../Shop/Shop");
        }
    }
}
