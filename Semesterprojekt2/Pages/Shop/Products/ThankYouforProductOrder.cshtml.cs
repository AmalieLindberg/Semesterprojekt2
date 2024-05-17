using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Semesterprojekt2.Pages.Shop.Products
{
    public class ThankYouforProductOrderModel : PageModel
    {

        [BindProperty]
        public Models.Users Users { get; set; }

        [BindProperty]
        public Models.Shop.ProductOrder Order { get; set; }

        public ThankYouforProductOrderModel()
        { }

        public void OnGet()
        {
        }
    }
}
