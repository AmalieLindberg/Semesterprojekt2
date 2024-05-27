using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Semesterprojekt2.Pages.Login;
using Semesterprojekt2.Service.BookATimeService;
using Semesterprojekt2.Service.UserService.UserService;

namespace Semesterprojekt2.Pages.Orderhistory.BookATimeOrder
{
    public class DeleteOrderModel : PageModel
    {
        private IBookATimeService _BookATimeService;
        private IUserService _UserService;

        [BindProperty]
        public Models.BookATime.BookATime BookATime { get; set; }
        public Models.Users Users { get; set; }

        public DeleteOrderModel(IBookATimeService bookATimeService, IUserService userService)
        {
            _BookATimeService = bookATimeService;
            _UserService = userService;
        }
        public IActionResult OnGet(int id)
        {
            int userid = LoginModel.LoggedInUser.UserId;
            Users = _UserService.GetUser(userid);
            BookATime = _BookATimeService.GetBookATimeById(id);
            if (BookATime == null)
                return RedirectToPage("/Index"); 

            return Page();
        }
        public async Task<IActionResult> OnPost()
        {
            Models.BookATime.BookATime deletedBookATime = await _BookATimeService.DeleteBookATime(BookATime.Id);
            if (deletedBookATime == null)
                return RedirectToPage("/Error/Error");
            return RedirectToPage("/Orderhistory/BookATimeOverviw");
        }
    }
}
