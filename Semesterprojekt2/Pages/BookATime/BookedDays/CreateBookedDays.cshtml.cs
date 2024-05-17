using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Semesterprojekt2.Models;
using Semesterprojekt2.Pages.Login;
using Semesterprojekt2.Service.BookATimeService;
using Semesterprojekt2.Service.UserService.UserService;

namespace Semesterprojekt2.Pages.BookATime.BookedDays
{
    [Authorize(Roles = "Admin")]
    public class CreateBookedDaysModel : PageModel
    {
        [BindProperty]
        public Models.BookATime.BookedDays BookedDays { get; set; }
        private BookedDaysService BookedDaysService;
        private IUserService userService;
        public Models.Users User { get; set; }
        public CreateBookedDaysModel(BookedDaysService bookedDaysService, IUserService service)
        {
            BookedDaysService = bookedDaysService;
            userService = service;
        }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                
                return Page();
            }
            int id = LoginModel.LoggedInUser.UserId;
            User = userService.GetUser(id);
            BookedDays.UserId = id;
            await BookedDaysService.AddBookedDays(BookedDays);
            return RedirectToPage("/BookATime/Calendar");
        }
    }
}
