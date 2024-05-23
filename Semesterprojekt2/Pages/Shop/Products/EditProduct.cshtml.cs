using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Semesterprojekt2.Models.Shop;
using Semesterprojekt2.Service;

namespace Semesterprojekt2.Pages.Shop.Products
{
	// Denne klasse håndterer logikken bag redigering af et eksisterende produkt.
	public class EditProductModel : PageModel
    {
		// Reference til en service, der styrer produktrelaterede dataoperationer.
		private IProductService _productService;

		// _webHostEnvironment bruges til at få adgang til serverens filsystem.
		private IWebHostEnvironment _webHostEnvironment;

		// Konstruktør der initialiserer services via dependency injection.
		public EditProductModel(IProductService productService, IWebHostEnvironment webHost)
		{
			_productService = productService;
			_webHostEnvironment = webHost;
		}

		// Denne property binder automatisk modellen fra en HTTP-forespørgsel til produktet.
		[BindProperty]
        public Models.Shop.Product Product { get; set; }

		// BindProperty for at håndtere filupload af produktbilleder.
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
			if (!ModelState.IsValid)
			{
				return Page();
			}
			if (Photo != null && Product != null)
			{
				if (Product.ProductImage != null)
				{
					string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "/Images", "Shop", Product.ProductImage);
					System.IO.File.Delete(filePath);
				}
				Product.ProductImage = ProcessUploadedFile();
			}
			if (Product != null) { await _productService.UpdateProductAsync(Product); }
			return RedirectToPage("/Shop/Shop");
		}

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
