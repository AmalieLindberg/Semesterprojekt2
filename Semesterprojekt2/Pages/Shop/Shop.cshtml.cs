using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Semesterprojekt2.Models;
using Semesterprojekt2.Models.Shop;
using Semesterprojekt2.Service;

namespace Semesterprojekt2.Pages.Shop
{
    // Denne klasse h�ndterer logikken bag shop-siden, hvor produkter vises.
    public class ShopModel : PageModel
    {
        // Reference til en service, der styrer produktrelaterede dataoperationer.
        private IProductService _productService;

        private readonly ShoppingCartService _shoppingCartService; // The service where GetCartItem() is defined

        // Konstrukt�r, der initialiserer productService via dependency injection.
        public ShopModel(IProductService productService, ShoppingCartService shoppingCartService)
        {
            _productService = productService;
            _shoppingCartService = shoppingCartService;
        }

        private List<CartItem>? CartItems { get; set; }

        // Liste af produkter der skal vises p� siden.
        public List<Product>? Products { get; private set; }

        // BindProperty markerer disse properties, s� de automatisk binder ved POST-foresp�rgsler.
        [BindProperty]
		public string SearchString { get; set; } // Tekststreng for s�gefeltet.

        [BindProperty]
		public int MinPrice { get; set; } // Minimums pris for prisfiltrering.

        [BindProperty]
		public int MaxPrice { get; set; } // Maksimums pris for prisfiltrering.

        // OnGet metoden kaldes ved GET-anmodninger og bruges til at indl�se alle produkter.
        public void OnGet()
        {
            // Henter alle produkter fra produkt service.
            Products = _productService.GetProducts();
        }

        // OnPostNameSearch metoden h�ndterer POST-anmodninger fra s�geformularen.
        public IActionResult OnPostNameSearch()
        {
            // Henter produkter baseret p� s�gestrengen.
            Products = _productService.NameSearch(SearchString).ToList();
            // Returnerer den aktuelle side med de opdaterede produktresultater.
            return Page();
		}

        // OnPostPriceFilter metoden h�ndterer POST-anmodninger fra prisfiltreringsformularen.
        public IActionResult OnPostPriceFilter()
        {
            // Henter produkter inden for det specificerede prisinterval.
            Products = _productService.PriceFilter(MaxPrice, MinPrice).ToList();
            // Returnerer den aktuelle side med de opdaterede produktresultater.
            return Page();
		}

        public IActionResult OnPostAddToCart(int productId, int quantity)
        {
            // Assume ProductService is available through dependency injection and used to fetch the product
            var product = _productService.GetProduct(productId); // Fetch the product details from the service

            if (product == null)
            {
                // Handle the case where the product does not exist
                return RedirectToPage("/Error"); // or another appropriate error handling approach
            }

            // Add to cart
            _shoppingCartService.AddToCart(product, 1);

            return RedirectToPage("/Shop/Shop"); // Redirect to the cart page or confirmation page
        }
    }
}
