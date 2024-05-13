using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Semesterprojekt2.Models;
using Semesterprojekt2.Service.UserService.UserService;
using Semesterprojekt2.Service;

namespace Semesterprojekt2.Pages.Orderhistory
{
    public class ProductOrderHistoryModel : PageModel
    {
        private IProductService _productService;
        private UserService _userService;
        private ProductOrderService _productOrderService;


        public Models.Shop.Product Product { get; set; }

        public Users Users { get; set; }

        public Models.Shop.ProductOrder ProductOrder { get; set; } = new Models.Shop.ProductOrder();


        [BindProperty]
        public int Count { get; set; }


        public ProductOrderHistoryModel(IProductService productService, UserService userService, ProductOrderService productOrderService)
        {
            _productService = productService;
            _userService = userService;
            _productOrderService = productOrderService;
        }


        public void OnGet(int id)
        {
            Product = _productService.GetProduct(id);
            Users = _userService.GetUserByUserName(HttpContext.User.Identity.Name);
        }

        public IActionResult OnPost(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            Product = _productService.GetProduct(id);
            Users = _userService.GetUserByUserName(HttpContext.User.Identity.Name);
            ProductOrder.UserId = Users.UserId;
            ProductOrder.ProductId = Product.Id;
            ProductOrder.Date = DateTime.Now;
            ProductOrder.Count = Count;
            _productOrderService.AddOrder(ProductOrder);
            return RedirectToPage("../Shop/Shop");
        }
    }
}
