using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Semesterprojekt2.Models.Shop;
using Semesterprojekt2.Service;

namespace Semesterprojekt2.Pages.Shop.Products
{
	// Klassen AddProductModel styrer tilf�jelsen af nye produkter via en webformular.
	public class AddProductModel : PageModel
	{
		// _productService h�ndterer forretningslogik relateret til produktdata.
		private IProductService _productService;
		// _webHostEnvironment bruges til at f� adgang til serverens filsystem.
		private IWebHostEnvironment _webHostEnvironment;

		// Konstrukt�r der initialiserer services via dependency injection.
		public AddProductModel(IProductService productService, IWebHostEnvironment webHost)
        {
            _productService = productService;
			_webHostEnvironment = webHost;
		}

		// BindProperty attributten binder modeldata automatisk fra HTTP-anmodninger.
		[BindProperty]
        public Models.Shop.Product Product { get; set; }

		// BindProperty for at h�ndtere filupload af produktbilleder.
		[BindProperty]
		public IFormFile? Photo { get; set; }

		// OnGet metoden kaldes, n�r siden anmodes via GET. Den returnerer siden selv.
		public IActionResult OnGet()
        {
            return Page();
        }

		// OnPostAsync metoden h�ndterer POST-anmodninger, som sker n�r formularen indsendes.
		public async Task<IActionResult> OnPostAsync()
		{
			// Tjekker at ModelState er gyldig (alle form-krav er opfyldt).
			if (!ModelState.IsValid)
			{
				return Page();
			}
			// Hvis der er uploadet et foto, behandles og gemmes dette.
			if (Photo != null)
			{
				// Hvis der allerede er et billede tilknyttet produktet, slettes det gamle f�rst.
				if (Product.ProductImage != null)
				{
					string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "/Images", "Shop", Product.ProductImage);
					System.IO.File.Delete(filePath);
				}
				// ProcessUploadedFile h�ndterer oprettelse af filnavn og gemning af filen.
				Product.ProductImage = ProcessUploadedFile();
			}
			// Tilf�j det nye produkt til databasen via ProductService.
			await _productService.AddProductAsync(Product);
			// Redirect til butikssiden efter succesfuld tilf�jelse.
			return RedirectToPage("/Shop/Shop");
		}

		// Hj�lpemetode til at gemme det uploadede billede og returnere filnavnet.
		private string ProcessUploadedFile()
		{
			string uniqueFileName = null;
			if (Photo != null)
			{
				string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Images", "Shop");
				uniqueFileName = Guid.NewGuid().ToString() + "_" + Photo.FileName;
				string filePath = Path.Combine(uploadsFolder, uniqueFileName);
				using (var fileStream = new FileStream(filePath, FileMode.Create))
				{
					Photo.CopyTo(fileStream);
				}
			}
			return uniqueFileName;
		}
	}
}
