using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Semesterprojekt2.Service.UserService.UserService;
using Semesterprojekt2.Service;
using Semesterprojekt2.Models.Shop;
using Semesterprojekt2.Pages.Login;
using Semesterprojekt2.Models;


namespace Semesterprojekt2.Pages.Orderhistory
{
    public class ProductOrderHistoryModel : PageModel
    {
        public IUserService _userService { get; set; }
        public ProductOrderService _productOrderService { get; set; }
        public IProductService _productService { get; set; }

        public IEnumerable<ProductOrder> MyProductOrders { get; set; }


        public ProductOrderHistoryModel(IUserService userService, ProductOrderService productOrderService, IProductService productService)
        {
            _userService = userService;
            _productOrderService = productOrderService;
            _productService = productService;
        }

        public void OnGet()
        {
            int id = LoginModel.LoggedInUser.UserId;
            Users CurrentUser = _userService.GetUser(id);
            MyProductOrders = _userService.GetUserProductOrders(CurrentUser).ProductOrders;
            foreach (var order in MyProductOrders)
            {
                order.User = _userService.GetUser(order.UserId);
                order.Product = _productService.GetProduct(order.ProductId);
            }
        }
    }
}
