using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Semesterprojekt2.Service.UserService.UserService;
using System.Diagnostics;

namespace Semesterprojekt2.Pages.Login
{
    public class SignUpModel : PageModel
    {
        private UserService _userService;

        [BindProperty]
        public Models.User User { get; set; }

        public SignUpModel(UserService userService)
        {
            _userService = userService;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public IActionResult OnPost()
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
            _userService.AddUser(User);
            return RedirectToPage("Login");
        }



    }
}

