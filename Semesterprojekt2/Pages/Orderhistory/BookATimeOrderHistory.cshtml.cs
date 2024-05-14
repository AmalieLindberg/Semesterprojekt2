using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Semesterprojekt2.Models;
using Semesterprojekt2.Pages.Login;
using Semesterprojekt2.Service.BookATimeService;
using Semesterprojekt2.Service.UserService.UserService;


namespace Semesterprojekt2.Pages.Orderhistory
{
    public class OrderhistoryModel : PageModel
    {
        private IUserService UserService { get; set; }
        private IBookATimeService _bookATimeService { get; set; }
        public IEnumerable<Models.BookATime.BookATime> MyBookATimeOrder { get; set; }
     
        public OrderhistoryModel(IBookATimeService bookATimeService, IUserService userService)
        {
            _bookATimeService = bookATimeService;
            UserService = userService;
        }
        public IActionResult OnGet()
        {
            int id = LoginModel.LoggedInUser.UserId;
            Users CurrentUser = UserService.GetUser(id);
            MyBookATimeOrder = UserService.GetUserTidsbestillingOrders(CurrentUser).BookATime;
            return Page();

        }

}
}
