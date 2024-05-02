using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Semesterprojekt2.Models;
using Semesterprojekt2.Service.UserService.UserService;



namespace Semesterprojekt2.Pages.Login
{
    [Authorize(Roles = "Admin")] //Access to page only allowed if logged in as Admin
    public class UserOverviewModel : PageModel
    {
        //used to interact with the user service (contains CRUD methods)
        private IUserService _userService;
       
        public List<Models.Users>? Users { get; private set; }

        //dependency injection to retrieve user data
        public UserOverviewModel(IUserService userService)
        {
            _userService = userService;
        }

        //event handler for the HTTP GET request. It's invoked when the page is requested via a GET request.
        //In this method, it retrieves the list of users using the _userService and assigns it to the Users property.
        //This data will be used to populate the page with user information
        public void OnGet()
        {
            Users = _userService.GetUsers();
        }
    }
}
