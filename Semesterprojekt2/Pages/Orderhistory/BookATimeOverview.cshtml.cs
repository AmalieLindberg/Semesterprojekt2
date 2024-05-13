using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Semesterprojekt2.Service.UserService.UserService;
using Semesterprojekt2.Service;
using Semesterprojekt2.Service.BookATimeService;

namespace Semesterprojekt2.Pages.Orderhistory
{
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
        public IEnumerable<Models.BookATime.BookATime>? BookATimes { get; set; }

        public void OnGet()
        {
            BookATimes = _bookATimeService.GetAllBookATimeList();
            foreach (var tidsbestilling in BookATimes)
            {
                tidsbestilling.User = _userService.GetUser(tidsbestilling.UserId);
            }
            foreach (var ydelse in BookATimes)
            {
                ydelse.Ydelse = _ydelseService.GetYdelseById(ydelse.Id);
            }
            foreach (var dog in BookATimes)
            {
                dog.Dog = _dogService.GetDogById(dog.Id);
            }

        }
    }
}
