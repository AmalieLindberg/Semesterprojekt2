using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Semesterprojekt2.Service.UserService.UserService;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Semesterprojekt2.Pages.Profil
{
    public class EditUserModel : PageModel
    {
        private IUserService _userService;
		private IWebHostEnvironment _webHostEnvironment;
		[BindProperty]
		public IFormFile? ProfileImages { get; set; }
		public EditUserModel(IUserService userService, IWebHostEnvironment webHost)
        {
            _userService = userService;
			_webHostEnvironment = webHost;
		}

        [BindProperty]
        public Models.Users? User{ get; set; }

		[BindProperty]
		public string? ConfirmPassword { get; set; }

		public IActionResult OnGet(int id)
        {
            User = _userService.GetUser(id);
            if (User == null)
                return RedirectToPage("/NotFound"); //NotFound er ikke defineret endnu

            return Page();
        }


		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}


			// Check if passwords match and validate password fields
			if (!string.IsNullOrEmpty(User.Password) || !string.IsNullOrEmpty(ConfirmPassword))
			{
				if (User.Password != ConfirmPassword)
				{
					ModelState.AddModelError("ConfirmPassword", "Passwords do not match");
					return Page();
				}

				if (string.IsNullOrEmpty(User.Password) || User.Password.Length < 6 ||
					!Regex.IsMatch(User.Password, @"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{6,}$"))
				{
					ModelState.AddModelError("User.Password", "Password must be at least 6 characters long, and contain at least one letter and one number.");
					return Page();
				}
			}

			// Handle profile image upload
			if (ProfileImages != null)
			{
				if (User.ProfileImages != null)
				{
					string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "Images", User.ProfileImages);
					System.IO.File.Delete(filePath);
				}
				User.ProfileImages = ProcessUploadedFile();
			}

			await _userService.UpdateUser(User);

			// Confirmation message
			TempData["Message"] = $"Details for {User.Name} have been updated.";
			return Page();
		}



		private string ProcessUploadedFile()
		{
			string uniqueFileName = null;
			if (ProfileImages != null)
			{
				string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Images", "Profil");
				uniqueFileName = Guid.NewGuid().ToString() + "_" + ProfileImages.FileName;

				string filePath = Path.Combine(uploadsFolder, uniqueFileName);

				using (var fileStream = new FileStream(filePath, FileMode.Create))
				{ ProfileImages.CopyTo(fileStream); }
			}
			return uniqueFileName;
		}
	}
}
