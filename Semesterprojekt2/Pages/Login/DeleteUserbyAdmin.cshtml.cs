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
		public Models.Users? user { get; set; }

		public IActionResult OnGet(int id)
		{
			user = _userService.GetUser(id);
			if (user == null)
				return RedirectToPage("/NotFound"); 

			return Page();
		}

		public async Task<IActionResult> OnPost(int id)
		{
			Models.Users deletedUser = await _userService.DeleteUser(id);
			if (deletedUser == null)
				return RedirectToPage("/NotFound"); 

			TempData["Message"] = $"User {deletedUser.Name} successfully deleted.";
			user = null; // Setting user to null so that "Are you sure you want to delete user" box disappears
			
			return Page();
			
		}





	}
}
