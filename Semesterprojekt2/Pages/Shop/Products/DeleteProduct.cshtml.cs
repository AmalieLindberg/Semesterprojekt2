using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Semesterprojekt2.Service;
using Semesterprojekt2.Models.Shop;

namespace Semesterprojekt2.Pages.Shop.Products
{
	// Denne klasse håndterer logikken bag at slette et produkt fra databasen.
	public class DeleteProductModel : PageModel
    {
		// En reference til ProductService, som håndterer databaselaget for produktoperationer.
		private IProductService _productService;

		// Konstruktør der initialiserer productService gennem dependency injection.
		public DeleteProductModel(IProductService productService)
        {
            _productService = productService;
        }

		// BindProperty markerer denne property, så den automatisk binder ved POST-forespørgsler.
		[BindProperty]
        public Models.Shop.Product Product { get; set; }

		// OnGet metoden kaldes ved GET-forespørgsler og bruges til at indlæse produktet fra databasen.
		public IActionResult OnGet(int id)
        {
            // Henter produktet baseret på den angivne id.
            Product = _productService.GetProduct(id);
			// Hvis produktet ikke findes, omdirigeres brugeren til en 'Ikke fundet' side.
			if (Product == null)
                return RedirectToPage("/NotFound");
			// Returnerer siden, hvis produktet findes.
			return Page();
        }

		// OnPost metoden kaldes ved POST-forespørgsler og håndterer sletning af produktet.
		public IActionResult OnPost()
		{
			// Kalder DeleteProduct på productService for at slette det givne produkt.
			Models.Shop.Product deletedProduct = _productService.DeleteProduct(Product.Id);
			// Hvis der ikke findes et produkt at slette, omdirigeres brugeren til 'Ikke fundet' siden.
			if (deletedProduct == null)
                return RedirectToPage("/NotFound");
			// Efter succesfuld sletning omdirigeres brugeren til shop-siden.
			return RedirectToPage("/Shop/Shop");
        }
    }
}
