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
        //private IProductService _productService;
        private IUserService _userService;
        private ProductOrderService _orderService;

        public ShoppingCartModel(ShoppingCartService shoppingCartService/*, IProductService productService*/, IUserService userService, ProductOrderService productOrderService)
        {
            _shoppingCartService = shoppingCartService;
            //_productService = productService;
            _userService = userService;
            _orderService = productOrderService;
            
        }

        public List<Models.CartItem> CartItem { get; set; } = new List<CartItem>();
        //public Models.Shop.Product Product { get; set; }
        //public Models.CartItem CartItems { get; set; }
     
        public Users User { get; set; }
       
        public Models.Shop.ProductOrder Order { get; set; }

        //public int Count { get; set; }

        public void OnGet()
        {
            //int id = LoginModel.LoggedInUser.UserId;
            //Users CurrentUser = _userService.GetUser(id);
            CartItem = _shoppingCartService.GetAllCartItems();
            //Product = _productService.GetProduct(id);
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
        
        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            int id = LoginModel.LoggedInUser.UserId;
            User = _userService.GetUser(id);
           
            foreach (var item in _shoppingCartService.GetAllCartItems())
            {
                Order = new Models.Shop.ProductOrder();
                Order.ProductId = item.Product.Id; 
                Order.UserId = User.UserId;
                Order.Counts = item.Quantity;
                Order.Date = DateTime.Now;

                
                await _orderService.AddOrderAsync(Order);
            }
            //Slet alt i indkøbskurv efter
            _shoppingCartService.GetAllCartItems().Clear();
       
            return RedirectToPage("../Shop/Products/ThankYouforProductOrder", new {id=Order.OrderId});
        }
    }
}
