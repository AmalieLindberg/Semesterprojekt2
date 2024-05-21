using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Semesterprojekt2.Models.Shop;
using Semesterprojekt2.Models;
using Semesterprojekt2.Pages.Login;
using Semesterprojekt2.Service.UserService.UserService;
using Semesterprojekt2.Service;

namespace Semesterprojekt2.Pages.Orderhistory
{
    [Authorize(Roles = "Admin")]
    public class ProductOrderOverviewModel : PageModel
    {
        public IUserService _userService { get; set; }
        public ProductOrderService _productOrderService { get; set; }
        public IProductService _productService { get; set; }

        public IEnumerable<Models.Shop.ProductOrder> MyProductOrders { get; set; }

        [BindProperty]
        public Models.Shop.ProductOrder ProductOrder { get; set; }

        public ProductOrderOverviewModel(IUserService userService, ProductOrderService productOrderService, IProductService productService)
        {
            _userService = userService;
            _productOrderService = productOrderService;
            _productService = productService;
        }

        public void OnGet()
        {
            MyProductOrders = _productOrderService.GetAllProductOrders();
			foreach (var order in MyProductOrders)
            {
                order.User = _userService.GetUser(order.UserId);
                order.Product = _productService.GetProduct(order.ProductId);
            }
        }
    }
}
