using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Semesterprojekt2.Service.UserService.UserService;
using Semesterprojekt2.DAO;
using Semesterprojekt2.Models;
using Semesterprojekt2.Pages.Login;
using Semesterprojekt2.Service;


namespace Semesterprojekt2.Pages.Orderhistory
{
    public class ProductOrderHistoryModel : PageModel
    {
        public UserService _userService { get; set; }

        public IEnumerable<ProductOrderDAO> MyProductOrders { get; set; }


        public ProductOrderHistoryModel(UserService userService)
        {
            _userService = userService;
        }
        public IActionResult OnGet()
        {
            int id = LoginModel.LoggedInUser.UserId;
            Users CurrentUser = _userService.GetUser(id);
            MyProductOrders = _userService.GetUserProductOrders(CurrentUser);
            return Page();
        }
    }
}
