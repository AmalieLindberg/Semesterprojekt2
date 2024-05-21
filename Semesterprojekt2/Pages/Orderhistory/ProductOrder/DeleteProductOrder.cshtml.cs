using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Semesterprojekt2.Service;

namespace Semesterprojekt2.Pages.Orderhistory.ProductOrder
{
    public class DeleteProductOrderModel : PageModel
    {
		private ProductOrderService _productOrderService;

		// Konstrukt�r der initialiserer productService gennem dependency injection.
		public DeleteProductOrderModel(ProductOrderService productOrderService)
		{
			_productOrderService = productOrderService;
		}

		// BindProperty markerer denne property, s� den automatisk binder ved POST-foresp�rgsler.
		[BindProperty]
		public Models.Shop.ProductOrder ProductOrder { get; set; }

		// OnGet metoden kaldes ved GET-foresp�rgsler og bruges til at indl�se produktet fra databasen.
		public IActionResult OnGet(int id)
		{
			// Henter produktet baseret p� den angivne id.
			ProductOrder = _productOrderService.GetProductOrderById(id);
			// Hvis produktet ikke findes, omdirigeres brugeren til en 'Ikke fundet' side.
			if (ProductOrder == null)
				return RedirectToPage("/Error/Error");
			// Returnerer siden, hvis produktet findes.
			return Page();
		}

		// OnPost metoden kaldes ved POST-foresp�rgsler og h�ndterer sletning af produktet.
		public async Task<IActionResult> OnPost()
		{
			// Kalder DeleteProduct p� productService for at slette det givne produkt.
			Models.Shop.ProductOrder deletedProductOrder = await _productOrderService.DeleteProductOrderAsync(ProductOrder.OrderId);
			// Hvis der ikke findes et produkt at slette, omdirigeres brugeren til 'Ikke fundet' siden.
			if (deletedProductOrder == null)
				return RedirectToPage("/Error/Error");
			return RedirectToPage("/Orderhistory/ProductOrderOverview");
		}
	}
}
