using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Semesterprojekt2.Service;

namespace Semesterprojekt2.Pages.Profil
{
    public class DeleteUserModel : PageModel
    {
        private IUserService _userService;

        public DeleteUserModel(IUserService userService)
        {
            _userService = userService;
        }

        [BindProperty]
        public Models.User user { get; set; }


        public IActionResult OnGet(int id)
        {
            user = _userService.GetUser(id);
            if (user == null)
                return RedirectToPage("/NotFound"); //NotFound er ikke defineret endnu

            return Page();
        }

        public IActionResult OnPost()
        {
            Models.User deletedUser = _userService.DeleteUser(user.UserId);
            if (deletedUser == null)
                return RedirectToPage("/NotFound"); //NotFound er ikke defineret endnu

            return RedirectToPage("/Shop/Shop");
        }
    }
}
