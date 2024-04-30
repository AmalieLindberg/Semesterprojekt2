using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Semesterprojekt2.Pages.Login
{
    public class LogOutModel : PageModel
    {

        public async Task<IActionResult> OnGet()
        {
            LoginModel.LoggedInUser = null;

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToPage("/index");
        }
    }
}