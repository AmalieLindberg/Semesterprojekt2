using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Semesterprojekt2.Service.UserService.UserService;
using Semesterprojekt2.Service;
using Semesterprojekt2.Service.BookATimeService;
using Microsoft.AspNetCore.Authorization;

namespace Semesterprojekt2.Pages.Orderhistory
{  [Authorize(Roles = "Admin")]
    public class BookATimeOverviewModel : PageModel
    {
      
        public BookATimeOverviewModel(IBookATimeService bookATimeService, IUserService userService, YdelseService ydelseService, DogService dogService)
        {
            _bookATimeService = bookATimeService;
            _userService = userService;
            _ydelseService = ydelseService;
            _dogService = dogService;
        }
        public IUserService _userService { get; set; }
        public YdelseService _ydelseService { get; set; }
        public DogService _dogService { get; set; }
        public IBookATimeService _bookATimeService { get; set; }
        public IEnumerable<Models.BookATime.BookATime> BookATimes { get; set; }

        public void OnGet()
        {
            BookATimes = _bookATimeService.GetAllBookATimeList();
            foreach (var order in BookATimes)
            {
                order.User = _userService.GetUser(order.UserId);
                order.Ydelse = _ydelseService.GetYdelseById(order.YdelseId);
                order.Dog = _dogService.GetDogById(order.DogId);  
              
            }

        }
    }
}
