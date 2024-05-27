using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Semesterprojekt2.Service;

namespace Semesterprojekt2.Pages.Shop.Products
{
    public class DetailProductModel : PageModel
    {
        private IProductService _productService;

        private ShoppingCartService _shoppingCartService;


        public DetailProductModel(IProductService productService, ShoppingCartService shoppingCartService)
        {
            _productService = productService;
            _shoppingCartService = shoppingCartService;
        }

        [BindProperty]
        public Models.Shop.Product Product { get; set; }

        public IActionResult OnGet(int id)
        { 
            Product = _productService.GetProduct(id);
            if (Product == null)
                return RedirectToPage("/Error/Error");

            return Page();
        }

        public IActionResult OnPostAddToCart(int productId, int quantity)
        {

            var product = _productService.GetProduct(productId);
            if (product == null)
            {

                return RedirectToPage("/Error/Error"); 
            }
            // Tilføjer produktet til indkøbskurven med specificeret antal
            _shoppingCartService.AddToCart(product, 1);

            return RedirectToPage("/Shop/Shop");
        }
    }
}
