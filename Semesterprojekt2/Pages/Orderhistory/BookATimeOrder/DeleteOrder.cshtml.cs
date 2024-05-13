using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Semesterprojekt2.Service.BookATimeService;

namespace Semesterprojekt2.Pages.Orderhistory.BookATimeOrder
{
    public class DeleteOrderModel : PageModel
    {
        private IBookATimeService _BookATimeService;

        [BindProperty]
        public Models.BookATime.BookATime BookATime { get; set; }

        public DeleteOrderModel(IBookATimeService bookATimeService)
        {
            _BookATimeService = bookATimeService;
        }
        public IActionResult OnGet(int id)
        {
            BookATime = _BookATimeService.GetBookATimeById(id);
            if (BookATime == null)
                return RedirectToPage("/Index"); 

            return Page();
        }
        public async Task<IActionResult> OnPost()
        {
            Models.BookATime.BookATime deletedBookATime = await _BookATimeService.DeleteBookATime(BookATime.Id);
            if (deletedBookATime == null)
                return RedirectToPage("/Error/Error");
            return RedirectToPage("/Index");
        }
    }
}
