using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Semesterprojekt2.Service;

namespace Semesterprojekt2.Pages.Shop.Products
{
    // Denne klasse håndterer logikken bag at vise detaljeret information om et enkelt produkt.
    public class DetailProductModel : PageModel
    {
        // Denne property binder automatisk modellen fra en HTTP-forespørgsel til produktet.
        [BindProperty]
        public Models.Shop.Product Product { get; set; }

        // Reference til en service, der styrer produktrelaterede data.
        private IProductService _productService;

        // Konstruktør, der initialiserer productService via dependency injection.
        public DetailProductModel(IProductService productService)
        {
            _productService = productService;
        }

        // OnGet metoden kaldes ved GET-anmodninger. Den indlæser produktet baseret på et produkt-id.
        public IActionResult OnGet(int id)
        {
            // Henter et produkt baseret på id. Bruger _productService for at tilgå produktinformationerne.
            Product = _productService.GetProduct(id);
            // Hvis det anmodede produkt ikke findes, omdirigeres brugeren til en 'Ikke fundet' side.
            if (Product == null)
                return RedirectToPage("/NotFound"); //NotFound er ikke defineret endnu
            // Returnerer den aktuelle side med produktinformationen.
            return Page();
        }
    }
}
