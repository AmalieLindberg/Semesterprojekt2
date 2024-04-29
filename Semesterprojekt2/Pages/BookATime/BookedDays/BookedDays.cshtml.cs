using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Semesterprojekt2.Service.BookATimeService;

namespace Semesterprojekt2.Pages.BookATime.BookedDays
{
    public class BookedDaysModel : PageModel
    {
        public BookedDaysModel(BookedDaysService bookedDaysService)
        {
            BookedDaysService = bookedDaysService;
        }

        public List<Models.BookATime.BookedDays> BookedDays { get; private set; }
        public BookedDaysService BookedDaysService { get; set; }
        public void OnGet()
        {
            BookedDays = BookedDaysService.GetBookedDaysList();
        }
    }
}
