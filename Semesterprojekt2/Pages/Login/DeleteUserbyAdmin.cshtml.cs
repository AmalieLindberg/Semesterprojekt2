using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Semesterprojekt2.Service.UserService.UserService;

namespace Semesterprojekt2.Pages.Login
{
    public class DeleteUserbyAdminModel : PageModel
    {
		private IUserService _userService;

		public DeleteUserbyAdminModel(IUserService userService)
		{
			_userService = userService;
		}

		[BindProperty]
		public Models.Users user { get; set; }

		public IActionResult OnGet(int id)
		{
			user = _userService.GetUser(id);
			if (user == null)
				return RedirectToPage("/NotFound"); //NotFound er ikke defineret endnu

			return Page();
		}

		/*
		 * Det her virker ikke rigtig. Jeg sletter altid useren jeg er logged ind med 
		 * (eller user med ID 0 - ved det ikke helt)
		 * 
		public IActionResult OnPost()
		{
			Models.Users deletedUser = _userService.DeleteUser(user.UserId);
			if (deletedUser == null)
				return RedirectToPage("/NotFound"); //NotFound er ikke defineret endnu

			return RedirectToPage("/Login/UserOverview");
		}
		*/




	}
}
