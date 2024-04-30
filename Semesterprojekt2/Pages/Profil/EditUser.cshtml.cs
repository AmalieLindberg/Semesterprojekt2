using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Semesterprojekt2.Service.UserService.UserService;

namespace Semesterprojekt2.Pages.Profil
{
    public class EditUserModel : PageModel
    {
        private IUserService _userService;

        public EditUserModel(IUserService userService)
        {
            _userService = userService;
        }

        [BindProperty]
        public Models.User User{ get; set; }

        public IActionResult OnGet(int id)
        {
            User = _userService.GetUser(id);
            if (User == null)
                return RedirectToPage("/NotFound"); //NotFound er ikke defineret endnu

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _userService.UpdateUser(User);
            return RedirectToPage("/Profil/Profil");
        }
    }
}