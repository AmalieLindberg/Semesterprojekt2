using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Semesterprojekt2.Pages.Login;
using Semesterprojekt2.Service;
using Semesterprojekt2.Service.UserService.UserService;
using System.Diagnostics;

namespace Semesterprojekt2.Pages.Profil.Dog
{
    public class AddDogModel : PageModel
    {
		private IWebHostEnvironment _webHostEnvironment;
		public Models.Users Users { get; set; }
        [BindProperty]
        public Models.Dog Dog { get; set; }
        [BindProperty]
        public IFormFile DogImage { get; set; }


		public IUserService _userService { get; set; }
        public DogService _dogService { get; set; }

        public AddDogModel(IUserService userService, DogService dogService, IWebHostEnvironment webHostEnvironment)
        {
            _userService = userService;
            _dogService = dogService;
            _webHostEnvironment = webHostEnvironment;

        }

        public void OnGet()
        {
            int id = LoginModel.LoggedInUser.UserId;
            Users = _userService.GetUser(id);
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

			if (DogImage != null)
			{
				if (Dog.DogImage != null)
				{
					string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "ImagesForBookATime", "Dog", Dog.DogImage);
					System.IO.File.Delete(filePath);
				}
				Dog.DogImage = ProcessUploadedFile();
			}
            int id = LoginModel.LoggedInUser.UserId;
            Users = _userService.GetUser(id);
            Dog.UserId = Users.UserId;
            await _dogService.AddDogAsync(Dog);
			

			return RedirectToPage("/Profil/Profil");
        }
		private string ProcessUploadedFile()
		{
			string uniqueFileName = null;
			if (DogImage != null)
			{
				string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "ImagesForBookATime", "Dog");
				uniqueFileName = Guid.NewGuid().ToString() + "_" + DogImage.FileName;

				string filePath = Path.Combine(uploadsFolder, uniqueFileName);

				using (var fileStream = new FileStream(filePath, FileMode.Create))
				{ DogImage.CopyTo(fileStream); }
			}
			return uniqueFileName;
		}
	}
}
