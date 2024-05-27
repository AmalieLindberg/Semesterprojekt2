using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Semesterprojekt2.Models.Shop;
using Semesterprojekt2.Service;

namespace Semesterprojekt2.Pages.Shop.Products
{
	public class AddProductModel : PageModel
	{
        // Afhængig af productService
        private IProductService _productService;

        // Afhængig af webHostEnvironment (leveres af ASP.NET Core)
        private IWebHostEnvironment _webHostEnvironment;

		public AddProductModel(IProductService productService, IWebHostEnvironment webHost)
        {
            _productService = productService;
			_webHostEnvironment = webHost;
		}

        // BindProperty til produktmodellen
        [BindProperty]
        public Models.Shop.Product Product { get; set; }

        // BindProperty til foto upload
        [BindProperty]
		public IFormFile? Photo { get; set; }

		//GetMetoden returnerer standard siden
		public IActionResult OnGet()
        {
            return Page();
        }

		public async Task<IActionResult> OnPostAsync()
		{
            // Tjekker om modeltilstanden er gyldig, og returnerer samme side, hvis model er ugyldig
            if (!ModelState.IsValid)
			{
				return Page();
			}
            // Hvis der er et billede uploadet
            if (Photo != null)
			{
                // Hvis produktet allerede har et billede, slettes det gamle billede
                if (Product.ProductImage != null)
				{
					string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "/Images", "Shop", Product.ProductImage);
					System.IO.File.Delete(filePath); // Sletter det gamle billede
                }
                // Behandler og gemmer det nye billede
                Product.ProductImage = ProcessUploadedFile();
			}
            // Tilføjer produktet ved hjælp af produktservicen
            await _productService.AddProductAsync(Product);
            // Omdirigerer til shopsiden efter produktet er tilføjet
            return RedirectToPage("/Shop/Shop");
		}

        // Hjælpemetode til at behandle det uploadede billede
        private string ProcessUploadedFile()
		{
            // Erklærer en variabel ved navn uniqueFileName af typen string og initialiserer den til null
            string uniqueFileName = null;
            // Hvis der er et billede uploadet
            if (Photo != null)
			{
                // Folder hvor billedet skal gemmes
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Images", "Shop");
                // Opretter et unikt filnavn
                uniqueFileName = Guid.NewGuid().ToString() + "_" + Photo.FileName;
                // Kombinerer folderen og det unikke filnavn for at få hele stien
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                // Opretter en filstream og kopierer billedet til stien
                using (var fileStream = new FileStream(filePath, FileMode.Create))
				{
					Photo.CopyTo(fileStream);
				}
			}
            // Returnerer det unikke filnavn
            return uniqueFileName;
		}
	}
}
