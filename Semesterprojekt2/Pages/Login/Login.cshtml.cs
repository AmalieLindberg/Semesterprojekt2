<<<<<<< Updated upstream
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Semesterprojekt2.Models;
using Semesterprojekt2.Service.UserService.UserService;
=======
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Semesterprojekt2.Models;
using Semesterprojekt2.Service;
>>>>>>> Stashed changes
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace Semesterprojekt2.Pages.Login
{
    public class LoginModel : PageModel
    {

        public static User LoggedInUser { get; set; } = null;
        private UserService _userService;

        [BindProperty]
        public string UserName { get; set; }

        [BindProperty, DataType(DataType.Password)]
        public string Password { get; set; }
        public string Message { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {

<<<<<<< Updated upstream
            List<User> users = _userService._users;
=======
            List<User> users = _userService.Users;
>>>>>>> Stashed changes
            foreach (User user in users)
            {

                if (UserName == user.Name && Password == user.Password)
                {

                    LoggedInUser = user;

                    var claims = new List<Claim> { new Claim(ClaimTypes.Name, UserName) };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                    return RedirectToPage("/Index");

                }

            }

            Message = "Invalid attempt";
            return Page();
        }
<<<<<<< Updated upstream
=======

>>>>>>> Stashed changes
    }

}
