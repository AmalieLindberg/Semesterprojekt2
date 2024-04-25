using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Semesterprojekt2.Service;

namespace Semesterprojekt2.Pages.Shop
{
    public class ShopModel : PageModel
    {
        private IProductService _productService;

        public ShopModel(IProductService productService)
        {
            _productService = productService;
        }

        public List<Models.Product>? Products { get; private set; }

        public void OnGet()
        {
            Products = _productService.GetProducts();
        }
    }
}
