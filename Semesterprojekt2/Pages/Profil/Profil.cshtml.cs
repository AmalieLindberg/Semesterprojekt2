using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Semesterprojekt2.Models;
using Semesterprojekt2.Models.Shop;
using Semesterprojekt2.Service;
using Semesterprojekt2.Service.UserService.UserService;
using Semesterprojekt2.Pages.Login;

namespace Semesterprojekt2.Pages.Profil
{
    public class ProfilModel : PageModel
    {
		[BindProperty]
		public Models.Users User { get; set; }
        public Models.Dog Dog { get; set; }
		private IUserService _userService;
        private DogService _dogService { get; set; }

        public ProfilModel(IUserService userService, DogService dogService)
        {
            _userService = userService;
            _dogService = dogService;
        }
        public List<Users> users { get; private set; }

        public IEnumerable<Models.Dog> MyDogs { get; set; }
        
        public IActionResult OnGet()
        { 
            int id = LoginModel.LoggedInUser.UserId;
            User = _userService.GetUser(id);    
            Users CurrentUser = _userService.GetUser(id);
            MyDogs = _userService.GetUserDogs(CurrentUser).Dog;
            if (User == null)
                return RedirectToPage("/Error/Error");
            return Page();

        }

    }
}
