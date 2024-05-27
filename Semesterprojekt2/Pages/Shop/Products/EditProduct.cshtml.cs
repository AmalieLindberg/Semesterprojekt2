using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Semesterprojekt2.Models.Shop;
using Semesterprojekt2.Service;

namespace Semesterprojekt2.Pages.Shop.Products
{
	public class EditProductModel : PageModel
    {
		private IProductService _productService;

		private IWebHostEnvironment _webHostEnvironment;

		public EditProductModel(IProductService productService, IWebHostEnvironment webHost)
		{
			_productService = productService;
			_webHostEnvironment = webHost;
		}

		[BindProperty]
        public Models.Shop.Product Product { get; set; }

		[BindProperty]
		public IFormFile? Photo { get; set; }

		public IActionResult OnGet(int id)
		{
			Product = _productService.GetProduct(id);
			if (Product == null)
				return RedirectToPage("/Error/Error"); 

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
