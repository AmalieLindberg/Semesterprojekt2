using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Semesterprojekt2.Service;
using Semesterprojekt2.Models.Shop;

namespace Semesterprojekt2.Pages.Shop.Products
{
    public class DeleteProductModel : PageModel
    {
        private IProductService _productService;

        public DeleteProductModel(IProductService productService)
        {
            _productService = productService;
        }

        [BindProperty]
        public Models.Shop.Product Product { get; set; }


        public IActionResult OnGet(int id)
        {
            Product = _productService.GetProduct(id);
            if (Product == null)
                return RedirectToPage("/NotFound"); //NotFound er ikke defineret endnu

            return Page();
        }

        public IActionResult OnPost()
        {
            Models.Shop.Product deletedProduct = _productService.DeleteProduct(Product.Id);
            if (deletedProduct == null)
                return RedirectToPage("/NotFound"); //NotFound er ikke defineret endnu

            return RedirectToPage("/Shop/Shop");
        }
    }
}
