using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Semesterprojekt2.Service;
using Semesterprojekt2.Models.Shop;

namespace Semesterprojekt2.Pages.Shop.Products
{
	// Denne klasse h�ndterer logikken bag at slette et produkt fra databasen.
	public class DeleteProductModel : PageModel
    {
		// En reference til ProductService, som h�ndterer databaselaget for produktoperationer.
		private IProductService _productService;

		// Konstrukt�r der initialiserer productService gennem dependency injection.
		public DeleteProductModel(IProductService productService)
        {
            _productService = productService;
        }

		// BindProperty markerer denne property, s� den automatisk binder ved POST-foresp�rgsler.
		[BindProperty]
        public Models.Shop.Product Product { get; set; }

		// OnGet metoden kaldes ved GET-foresp�rgsler og bruges til at indl�se produktet fra databasen.
		public IActionResult OnGet(int id)
        {
            // Henter produktet baseret p� den angivne id.
            Product = _productService.GetProduct(id);
			// Hvis produktet ikke findes, omdirigeres brugeren til en 'Ikke fundet' side.
			if (Product == null)
                return RedirectToPage("/NotFound");
			// Returnerer siden, hvis produktet findes.
			return Page();
        }

		// OnPost metoden kaldes ved POST-foresp�rgsler og h�ndterer sletning af produktet.
		public IActionResult OnPost()
		{
			// Kalder DeleteProduct p� productService for at slette det givne produkt.
			Models.Shop.Product deletedProduct = _productService.DeleteProduct(Product.Id);
			// Hvis der ikke findes et produkt at slette, omdirigeres brugeren til 'Ikke fundet' siden.
			if (deletedProduct == null)
                return RedirectToPage("/NotFound");
			// Efter succesfuld sletning omdirigeres brugeren til shop-siden.
			return RedirectToPage("/Shop/Shop");
        }
    }
}
