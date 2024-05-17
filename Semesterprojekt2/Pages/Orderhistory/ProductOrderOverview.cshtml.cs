using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Semesterprojekt2.Pages.Orderhistory
{
    [Authorize(Roles = "Admin")]
    public class ProductOrderOverviewModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
