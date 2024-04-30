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

		// Konstruktør, der initialiserer productService via dependency injection.
		public EditProductModel(IProductService productService)
        {
            _productService = productService;
        }

		// Denne property binder automatisk modellen fra en HTTP-forespørgsel til produktet.
		[BindProperty]
        public Models.Shop.Product Product { get; set; }

		// OnGet metoden kaldes ved GET-forespørgsler og bruges til at indlæse produktet fra databasen baseret på et produkt-id.
		public IActionResult OnGet(int id)
        {
            // Henter et produkt baseret på id. Bruger _productService for at tilgå produktinformationerne.
            Product = _productService.GetProduct(id);
			// Hvis det anmodede produkt ikke findes, omdirigeres brugeren til en 'Ikke fundet' side.
			if (Product == null)
                return RedirectToPage("/NotFound");
	        // Returnerer den aktuelle side med produktinformationen klar til redigering.
			return Page();
        }

		// OnPost metoden kaldes ved POST-forespørgsler, når formen indsendes, og håndterer opdatering af produktet.
		public IActionResult OnPost()
		{
			// Tjekker at ModelState er gyldig (alle form-krav er opfyldt).
			if (!ModelState.IsValid)
			{
				// Returnerer den samme side for at tillade korrektion af formularindtastninger.
				return Page();
			}
			// Opdaterer produktet i databasen via productService.
			_productService.UpdateProduct(Product);
			// Efter succesfuld opdatering omdirigeres brugeren til produktoverblikssiden.
			return RedirectToPage("/Shop/Shop");
        }
    }
}
