using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Semesterprojekt2.Service;

namespace Semesterprojekt2.Pages.Shop.Products
{
    public class DetailProductModel : PageModel
    {
        [BindProperty]
        public Models.Shop.Product Product { get; set; }

        private IProductService _productService;

        public DetailProductModel(IProductService productService)
        {
            _productService = productService;
        }


        public IActionResult OnGet(int id)
        {
            Product = _productService.GetProduct(id);
            if (Product == null)
                return RedirectToPage("/NotFound"); //NotFound er ikke defineret endnu

            return Page();
        }
    }
}
