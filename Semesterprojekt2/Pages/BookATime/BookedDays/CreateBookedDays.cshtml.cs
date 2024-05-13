using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Semesterprojekt2.Service.BookATimeService;

namespace Semesterprojekt2.Pages.BookATime.BookedDays
{
    [Authorize(Roles = "Admin")]
    public class CreateBookedDaysModel : PageModel
    {
        [BindProperty]
        public Models.BookATime.BookedDays BookedDays { get; set; }
        public BookedDaysService BookedDaysService { get; set; }
        public CreateBookedDaysModel(BookedDaysService bookedDaysService)
        {
            BookedDaysService = bookedDaysService;
        }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                
                return Page();
            }

            await BookedDaysService.AddBookedDays(BookedDays);
            return RedirectToPage("/BookATime/Calendar");
        }
    }
}
