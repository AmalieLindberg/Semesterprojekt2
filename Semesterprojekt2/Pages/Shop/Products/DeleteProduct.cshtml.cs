using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Semesterprojekt2.Service;
using Semesterprojekt2.Models.Shop;

namespace Semesterprojekt2.Pages.Shop.Products
{
	public class DeleteProductModel : PageModel
    {
        // Reference til productService
        private IProductService _productService;

        // Konstruktør, der initialiserer produktservicen via dependency injection
        public DeleteProductModel(IProductService productService)
        {
            _productService = productService;
        }

        // BindProperty til produktmodellen
        [BindProperty]
        public Models.Shop.Product Product { get; set; }

        public IActionResult OnGet(int id)
        {
            // Henter produktet fra produktservicen baseret på ID
            Product = _productService.GetProduct(id);

            // Hvis produktet ikke findes, omdirigerer til Error-siden
            if (Product == null)
                return RedirectToPage("/Error/Error");

            // Returnerer den aktuelle side
            return Page();
        }

        //OnPost metoden sletter produktet
		public async Task<IActionResult> OnPost()
		{
            // Sletter produktet ved hjælp af produktservicen
            Models.Shop.Product deletedProduct = await _productService.DeleteProductAsync(Product.Id);

            // Hvis produktet ikke findes, omdirigerer til Error-siden
            if (deletedProduct == null)
            return RedirectToPage("/Error/Error");

            // Omdirigerer til shop-siden efter sletningen
            return RedirectToPage("/Shop/Shop");
        }
    }
}
