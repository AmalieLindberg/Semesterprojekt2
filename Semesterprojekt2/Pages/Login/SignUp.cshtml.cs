using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Semesterprojekt2.Models;
using Semesterprojekt2.Service.UserService.UserService;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace Semesterprojekt2.Pages.Login
{
    public class SignUpModel : PageModel
    {
        private UserService _userService;
        private PasswordHasher<string> passwordHasher;


        [BindProperty]
        public string Name { get; set; }

        [BindProperty, DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Email already exists.")]
        public string Email { get; set; }

        [BindProperty]
        public string Phone { get; set; }

        [BindProperty, DataType(DataType.Password), MinLength(6, ErrorMessage = "Passwordet skal være mindst 6 tegn.")]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{6,}$", ErrorMessage = "Passwordet skal indeholde mindst ét bogstav og ét tal.")]
        public string Password { get; set; }

        [BindProperty, DataType(DataType.Password), MinLength(6, ErrorMessage = "Passwordet skal være mindst 6 tegn.")]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{6,}$", ErrorMessage = "Passwordet skal indeholde mindst ét bogstav og ét tal.")]
        [Compare("Password", ErrorMessage = "De indtastede passwords matcher ikke.")]
        public string ConfirmPassword { get; set; }

        [BindProperty]
        public string Role { get; set; }


        public SignUpModel(UserService userService)
        {
            _userService = userService;
            passwordHasher = new PasswordHasher<string>();
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public IActionResult OnPost()
        {
            
            if (!ModelState.IsValid)
            {
                // Log validation errors
                foreach (var modelStateKey in ModelState.Keys)
                {
                    var modelStateVal = ModelState[modelStateKey];
                    foreach (var error in modelStateVal.Errors)
                    {
                        Debug.WriteLine(error.ErrorMessage);
                    }
                }
                return Page();
            }

            foreach (var user in _userService.GetUsers())
            {
                if (user.Email == Email)
                {
                    ModelState.AddModelError(nameof(Email), "Email already exists.");
                    return Page();
                }
  
            }

            string hashedPassword = passwordHasher.HashPassword(null, Password);
           
            _userService.AddUser(new Users(hashedPassword, Name, Phone, Email, Role));
            return RedirectToPage("Login");
        }


    }


}

