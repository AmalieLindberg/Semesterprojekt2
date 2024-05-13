using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Semesterprojekt2.Models;
using Semesterprojekt2.Pages.Login;
using Semesterprojekt2.Service.BookATimeService;
using Semesterprojekt2.Service.UserService.UserService;

namespace Semesterprojekt2.Pages.BookATime
{
    public class ThankYouForYourBookingModel : PageModel

    {
        
        private IBookATimeService _bookATimeService { get; set; }
        private IUserService _userService { get; set; }

    [BindProperty]
    public Models.BookATime.BookATime BookATime { get; set; }
        [BindProperty]
        public Models.Users Users { get; set; }

        public ThankYouForYourBookingModel(IBookATimeService bookATimeService, IUserService userService)
        {
            _bookATimeService = bookATimeService;
            _userService = userService;
        }

        
        public IActionResult OnGet(int id)
        {
            int UserId = LoginModel.LoggedInUser.UserId;
            Users = _userService.GetUser(UserId);
            if (Users == null)
                return RedirectToPage("/Error/Error");

            BookATime = _bookATimeService.GetBookATimeById(id);
            if (BookATime == null)
                return RedirectToPage("/Error/Error");

            return Page();
        }
    }
}
