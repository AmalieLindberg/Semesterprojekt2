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
        private IUserService _userService;
        private ProductOrderService _orderService;

        public ShoppingCartModel(ShoppingCartService shoppingCartService, IUserService userService, ProductOrderService productOrderService)
        {
            _shoppingCartService = shoppingCartService;
            _userService = userService;
            _orderService = productOrderService;
            
        }

        public List<Models.CartItem> CartItem { get; set; } = new List<CartItem>();
     
        public Users User { get; set; }
       
        public Models.Shop.ProductOrder Order { get; set; }

        public void OnGet()
        {
            CartItem = _shoppingCartService.GetAllCartItems();
        }

        // OnPost til at fjerne en enkelt vare fra indkøbskurven
        public IActionResult OnPostRemoveSingleCartItem(int productId)
        {
            _shoppingCartService.DeleteCartItem(productId);
            return RedirectToPage();
        }

        // OnPost til at fjerne alle varer fra indkøbskurven
        public IActionResult OnPostRemoveAllCartItems()
        {
            _shoppingCartService.ClearCart();
            return RedirectToPage();
        }

        //Beregner den samlede pris for varer i indkøbskurven
        public decimal TotalPrice
        {
            get
            {
                return CartItem.Sum(item => (item.Product.Price ?? 0) * item.Quantity);
            }
        }

        //OnPost til at fuldføre købet og oprette en ordre
        public async Task<IActionResult> OnPost()
        {
            // Tjekker om model state er gyldig
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Henter alle varer i indkøbskurven
            var cartItems = _shoppingCartService.GetAllCartItems();
            if (cartItems == null || !cartItems.Any())
            {
                return RedirectToPage("/Error/Error");
            }

            // Henter brugerens ID fra den loggede ind bruger
            int id = LoginModel.LoggedInUser.UserId;
            User = _userService.GetUser(id);

            // Opretter en ordre for hver vare i indkøbskurven
            foreach (var item in _shoppingCartService.GetAllCartItems())
            {
                Order = new Models.Shop.ProductOrder();
                Order.ProductId = item.Product.Id; 
                Order.UserId = User.UserId;
                Order.Counts = item.Quantity;
                Order.Date = DateTime.Now;

                
                await _orderService.AddOrderAsync(Order);
            }

            // Tømmer indkøbskurven efter ordreoprettelse
            _shoppingCartService.GetAllCartItems().Clear();
       
            return RedirectToPage("../Shop/Products/ThankYouforProductOrder", new {id=Order.OrderId});
        }
    }
}
