using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Semesterprojekt2.Models;
using Semesterprojekt2.Models.Shop;
using Semesterprojekt2.Service;
using Semesterprojekt2.Service.UserService.UserService;
using Semesterprojekt2.Pages.Login;

namespace Semesterprojekt2.Pages.Profil
{
    public class ProfilModel : PageModel
    {
		[BindProperty]
		public Models.Users Users { get; set; }

		private IUserService _userService;

        public ProfilModel(IUserService userService)
        {
            _userService = userService;
        }
        public List<Users> users { get; private set; }


        public IActionResult OnGet()
        {
            int id = LoginModel.LoggedInUser.UserId;

            Users = _userService.GetUser(id);
            if (Users == null)
                return RedirectToPage("/Error/Error");
            return Page();

        }

    }
}
