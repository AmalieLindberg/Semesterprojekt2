using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Semesterprojekt2.Service.BookATimeService;
using System.Data;
using Microsoft.AspNetCore.Authorization;


namespace Semesterprojekt2.Pages.BookATime.BookedDays
{
   [Authorize(Roles = "Admin")]
    public class DeleteBookedDaysModel : PageModel
    {
        private BookedDaysService BookedDaysService;

        [BindProperty]
        public Models.BookATime.BookedDays BookedDays { get; set; }

        public DeleteBookedDaysModel(BookedDaysService bookedDaysService)
        {
            BookedDaysService = bookedDaysService;
        }
        public IActionResult OnGet(int id)
        {
            BookedDays = BookedDaysService.GetBookedDaysById(id);
            if (BookedDays == null)
                return RedirectToPage("/NotFound"); //NotFound er ikke defineret endnu

            return Page();
        }
        public async Task<IActionResult> OnPost()
        {
            Models.BookATime.BookedDays deletedItem = await BookedDaysService.RemoveBookedDaysById(BookedDays.Id);
            if (deletedItem == null)
                return RedirectToPage("/NotFound"); //NotFound er ikke defineret endnu
            return RedirectToPage("/BookATime/Calendar");
        }
    }
}
