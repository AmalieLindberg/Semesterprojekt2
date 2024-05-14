using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Semesterprojekt2.Models;
using Semesterprojekt2.Service.UserService.UserService;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Data;
using System.Runtime.ConstrainedExecution;
using Microsoft.AspNetCore.Identity;
using System.Diagnostics;


namespace Semesterprojekt2.Pages.Login
{
    public class LoginModel : PageModel
    {
        // Gemmer den aktuelle logget ind bruger
        public static Users LoggedInUser { get; set; } = null;
        // Reference til IUserService interface
        private IUserService _userService;
        //BindProperty til email-inputfeltet på login-siden
        [BindProperty]
        public string Email { get; set; }
        //Bindproperty til pasword-inputfeltet på login-siden markeret som passwrd type
        [BindProperty, DataType(DataType.Password)]
        public string Password { get; set; }
        //Besked der vises ved uyldig logins
        public string Message { get; set; }
        //Konstruktør, der modtager en instance af IUserService
        public LoginModel(IUserService userService)
        {
            _userService = userService;
        }
        //Metode kaldt ved HTTP GET-anmodning til login-siden, 
        public void OnGet()
        {
            //intet gøres 
        }
        //Metode kaldt ved HTTP POST-anmodning fra logind-formularen
        public async Task<IActionResult> OnPost()
        {
            //Hent en liste af brugere fra IUserService
            List<Users> users = _userService.GetUsers();
            //Gennemgå hver bruger i listen
            foreach (Users user in users)
            {
                //tjek om indtastet email matcher en bruger i lsten
                if (Email == user.Email)
                {

                    var passwordHasher = new PasswordHasher<string>();

					// Debug: Check the format of the hash we are verifying
					Console.WriteLine($"Hash from DB: {user.Password}");

					if (passwordHasher.VerifyHashedPassword(null, user.Password, Password) == PasswordVerificationResult.Success)
                    {
                        //Gem den logget ind bruger
                        LoggedInUser = user;

                        //Opret en claim for brugerens email
                        var claims = new List<Claim> { new Claim(ClaimTypes.Email, Email) };

                        //Hvis User har Role="admin" tilføjes et nyt Claim-objekt, hvor ClaimTypes.Role sættes til "Admin"
                        if (user.Role == "Admin")
                        {
                            claims.Add(new Claim(ClaimTypes.Role, "Admin"));
                        }

                        //Opret en ClaimIdentity med email-claimet
                        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                        //Log brugeren in ved at oprette et cookie og tiltøje en climIdentety
                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                        //Redirect til startsiden
                        return RedirectToPage("/Index");
                    }
                }

            }
            //Hvis ingen brugere blev fundet med de oplysninger, så for man en fejlbesked istedet
            Message = "Invalid attempt";
            return Page();
        }
    }

}
