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
        public IActionResult OnGetSortById()
        {
            BookedDays = BookedDaysService.SortById().ToList();
            return Page();
        }
        public IActionResult OnGetSortByIdDescending()
        {
            BookedDays = BookedDaysService.SortByIdDescending().ToList();
            return Page();
        }
        public IActionResult OnGetSortByStartDate()
        {
            BookedDays = BookedDaysService.SortByStartDate().ToList();
            return Page();
        }
        public IActionResult OnGetSortByStartDateDescending()
        {
            BookedDays = BookedDaysService.SortByStartDateDescending().ToList();
            return Page();
        }
        public IActionResult OnGetSortByEndDate()
        {
            BookedDays = BookedDaysService.SortByEndDate().ToList();
            return Page();
        }
        public IActionResult OnGetSortByEndDateDescending()
        {
            BookedDays = BookedDaysService.SortByEndDateDescending().ToList();
            return Page();
        }
    }
}
