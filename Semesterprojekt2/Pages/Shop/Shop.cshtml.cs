using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Semesterprojekt2.Models.Shop;
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

        public List<Product>? Products { get; private set; }

		[BindProperty]
		public string SearchString { get; set; }

		[BindProperty]
		public int MinPrice { get; set; }

		[BindProperty]
		public int MaxPrice { get; set; }

		public void OnGet()
        {
            Products = _productService.GetProducts();
        }

		public IActionResult OnPostNameSearch()
		{
			Products = _productService.NameSearch(SearchString).ToList();
			return Page();
		}

		public IActionResult OnPostPriceFilter()
		{
			Products = _productService.PriceFilter(MaxPrice, MinPrice).ToList();
			return Page();
		}
	}
}
