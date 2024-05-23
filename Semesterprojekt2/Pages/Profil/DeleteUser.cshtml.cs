using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Semesterprojekt2.Models;
using Semesterprojekt2.Pages.Login;
using Semesterprojekt2.Service;
using Semesterprojekt2.Service.BookATimeService;
using Semesterprojekt2.Service.UserService.UserService;

namespace Semesterprojekt2.Pages.Profil
{
    public class DeleteUserModel : PageModel
    {
        private IUserService _userService;
        private IBookATimeService _bookATimeService;
        private DogService _dogService;
        private ProductOrderService _productOrderService;

        public DeleteUserModel(IUserService userService, IBookATimeService bookATimeService, DogService dogService, ProductOrderService productOrderService)
        {
            _userService = userService;
            _bookATimeService = bookATimeService;
            _dogService = dogService;
            _productOrderService = productOrderService;
        }

        [BindProperty]
        public Models.Users user { get; set; }
        public Models.Dog Dog { get; set; }
		public IEnumerable<Models.Dog> MyDogs { get; set; }

		public IActionResult OnGet(int id)
        {
         
            user = _userService.GetUser(id);
            if (user == null)
                return RedirectToPage("/NotFound"); //NotFound er ikke defineret endnu

            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
			int id = LoginModel.LoggedInUser.UserId;
			Users CurrentUser = _userService.GetUser(id);
			MyDogs = _userService.GetUserDogs(CurrentUser).Dog;
			foreach (var dog in MyDogs)
            {
                List<Models.BookATime.BookATime> deletedDogInBookATime = await _bookATimeService.DeleteDogInBookATime(dog.Id);
				Models.Dog deletedDog = await _dogService.DeleteDog(dog.Id);
			}

            _bookATimeService.DeleteUserInBookATime(id);

            _productOrderService.DeleteUserInProductOrder(id);
            
            Models.Users deletedUser = await _userService.DeleteUser(id);
            if (deletedUser == null)
                return RedirectToPage("/Error/Error"); //NotFound er ikke defineret endnu

            return RedirectToPage("/Login/LogOut");
        }
    }
}
