using Microsoft.AspNetCore.Mvc.RazorPages;
using Semesterprojekt2.Models;
using Semesterprojekt2.Service;

namespace Semesterprojekt2.Pages.Profil
{
    public class ProfilModel : PageModel
    {
        private IUserService _userService;

        public ProfilModel(IUserService userService)
        {
            _userService = userService;
        }
        public List<User>? Users { get; private set; }

        public void OnGet()
        {
            Users = _userService.GetUsers();
        }
    }
}
