using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Semesterprojekt2.Models.Shop;
using Semesterprojekt2.Pages.Login;
using Semesterprojekt2.Service;
using Semesterprojekt2.Service.UserService.UserService;

namespace Semesterprojekt2.Pages.Shop.Products
{
    public class ThankYouforProductOrderModel : PageModel
    {

        public IUserService _userService {  get; set; }
        public ProductOrderService _productOrderService { get; set; }

        public ThankYouforProductOrderModel(IUserService userService, ProductOrderService productOrderService)
        { 
            _userService = userService;
            _productOrderService = productOrderService;
        }

        [BindProperty]
        public Models.Users Users { get; set; }

        [BindProperty]
        public Models.Shop.ProductOrder Order { get; set; }

        public IActionResult OnGet(int id)
        {
            // Henter brugerens ID fra den loggede ind bruger
            int UserId = LoginModel.LoggedInUser.UserId;

            // Henter brugeren fra userServicen baseret på brugerens ID
            Users = _userService.GetUser(UserId);

            // Hvis brugeren ikke findes, omdirigerer til Error-siden
            if (Users == null)
                return RedirectToPage("/Error/Error");

            // Henter ordren fra productOrderServicen baseret på ordrens ID
            Order = _productOrderService.GetProductOrderById(id);

            // Hvis ordren ikke findes, omdirigerer metoden til Error-siden
            if (Order == null)
                return RedirectToPage("/Error/Error");

            // Returnerer den aktuelle side
            return Page();
        }
    }
}
