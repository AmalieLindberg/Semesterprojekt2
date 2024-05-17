using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Semesterprojekt2.Service;

namespace Semesterprojekt2.Pages.Profil.Dog
{
    public class EditDogModel : PageModel
    {
		private DogService _dogService;
		private IWebHostEnvironment _webHostEnvironment;
		[BindProperty]
		public IFormFile? DogImages { get; set; }
		public EditDogModel(DogService dogService, IWebHostEnvironment webHost)
		{
			_dogService = dogService;
			_webHostEnvironment = webHost;
		}

		[BindProperty]
		public Models.Dog Dog { get; set; }

		public IActionResult OnGet(int id)
		{
			Dog = _dogService.GetDogById(id);
			if (Dog == null)
				return RedirectToPage("Error/Error"); //NotFound er ikke defineret endnu

			return Page();
		}
		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid)
			{

				return Page();
			}

			if (DogImages != null)
			{
				if (Dog.DogImage != null)
				{
					string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "Images", Dog.DogImage);
					System.IO.File.Delete(filePath);
				}
				Dog.DogImage = ProcessUploadedFile();
			}
			
			await _dogService.UpdateDog(Dog);
			return RedirectToPage("/Profil/Profil");
		}
		private string ProcessUploadedFile()
		{
			string uniqueFileName = null;
			if (DogImages != null)
			{
				string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Images", "Profil");
				uniqueFileName = Guid.NewGuid().ToString() + "_" + DogImages.FileName;

				string filePath = Path.Combine(uploadsFolder, uniqueFileName);

				using (var fileStream = new FileStream(filePath, FileMode.Create))
				{ DogImages.CopyTo(fileStream); }
			}
			return uniqueFileName;
		}
	}
}
