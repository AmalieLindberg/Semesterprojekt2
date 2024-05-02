using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Semesterprojekt2.Models.Shop;
using Semesterprojekt2.Service;

namespace Semesterprojekt2.Pages.Shop.Products
{
	// Denne klasse h�ndterer logikken bag redigering af et eksisterende produkt.
	public class EditProductModel : PageModel
    {
		// Reference til en service, der styrer produktrelaterede dataoperationer.
		private IProductService _productService;

		// _webHostEnvironment bruges til at f� adgang til serverens filsystem.
		private IWebHostEnvironment _webHostEnvironment;

		// Konstrukt�r der initialiserer services via dependency injection.
		public EditProductModel(IProductService productService, IWebHostEnvironment webHost)
		{
			_productService = productService;
			_webHostEnvironment = webHost;
		}

		// Denne property binder automatisk modellen fra en HTTP-foresp�rgsel til produktet.
		[BindProperty]
        public Models.Shop.Product Product { get; set; }

		// BindProperty for at h�ndtere filupload af produktbilleder.
		[BindProperty]
		public IFormFile? Photo { get; set; }

		public IActionResult OnGet(int id)
		{
			Product = _productService.GetProduct(id);
			if (Product == null)
				return RedirectToPage("/NotFound"); //NotFound er ikke defineret endnu

			return Page();
		}

		public async Task<IActionResult> OnPostAsync()
		{
			// Tjekker at ModelState er gyldig (alle form-krav er opfyldt).
			if (!ModelState.IsValid)
			{
				return Page();
			}
			// Hvis der er uploadet et foto, behandles og gemmes dette.
			if (Photo != null && Product != null)
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
			if (Product != null) { await _productService.UpdateProductAsync(Product); }
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
