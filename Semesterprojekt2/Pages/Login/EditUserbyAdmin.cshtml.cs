using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Semesterprojekt2.Service.UserService.UserService;
using System.Diagnostics;

namespace Semesterprojekt2.Pages.Login
{
    public class EditUserbyAdminModel : PageModel
    {
        private IUserService _userService;

        public EditUserbyAdminModel(IUserService userService)
        {
            _userService = userService;
        }

        [BindProperty]
        public Models.Users User { get; set; }

        public IActionResult OnGet(int id)
        {
            User = _userService.GetUser(id);
            if (User == null)
                return RedirectToPage("/NotFound"); //NotFound er ikke defineret endnu

            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
				// Udskriver fejl til output-vinduet eller logger dem.
				foreach (var modelStateKey in ModelState.Keys)
				{
					var modelStateVal = ModelState[modelStateKey];
					foreach (var error in modelStateVal.Errors)
					{
						// Her kan du logge fejlen eller se den i debug-vinduet
						Debug.WriteLine(error.ErrorMessage);
					}
				}
				return Page();
            }

			await _userService.UpdateUser(User);
            return RedirectToPage("/Login/UserOverview");
        }
    }
}
