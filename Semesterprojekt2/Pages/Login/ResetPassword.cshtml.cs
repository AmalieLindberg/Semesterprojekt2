using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Semesterprojekt2.Service.UserService.UserService;

namespace Semesterprojekt2.Pages.Login
{
    public class ResetPasswordModel : PageModel
    {
		private IUserService _userService;

		public ResetPasswordModel(IUserService userService)
		{
			_userService = userService;
		}

		[BindProperty]
		public Models.Users? User { get; set; }

		public IActionResult OnGet(int id)
		{
			User = _userService.GetUser(id);
			if (User == null)
				return RedirectToPage("/NotFound"); 

			return Page();
		}

        //Method to reset password
       
        
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            var resetResult = await _userService.ResetPassword(User);

			TempData["Message"] = $"Password for {User.Name} has been reset.";
			return Page();

			/*
            if (resetResult == null)
                return RedirectToPage("/NotFound");
           

            TempData["Message"] = $"Password for {User.Name} successfully reset";
            
			return RedirectToPage("/Login/UserOverview");

			*/

             
		}


	}
}
