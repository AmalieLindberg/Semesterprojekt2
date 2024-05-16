using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Semesterprojekt2.Service.UserService.UserService;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

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
        public Models.Users User{ get; set; }

		[BindProperty, DataType(DataType.Password), MinLength(6, ErrorMessage = "Passwordet skal v�re mindst 6 tegn.")]
		[RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{6,}$", ErrorMessage = "Passwordet skal indeholde mindst �t bogstav og �t tal.")]
		public string ConfirmPassword { get; set; }


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

			// Check if passwords match
			if (!string.IsNullOrEmpty(User.Password))
			{
				if (User.Password != ConfirmPassword)
				{
					ModelState.AddModelError("ConfirmPassword", "Passwords do not match");
					return Page();
				}
				// Proceed to update the password
			}



			if (ProfileImages != null)
			{
				if (User.ProfileImages != null) 
				{
					string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "Images", User.ProfileImages);
					System.IO.File.Delete(filePath);
				}
				User.ProfileImages = ProcessUploadedFile();
			}

          await  _userService.UpdateUser(User);

			//Confirmation Message that user has been edited
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
