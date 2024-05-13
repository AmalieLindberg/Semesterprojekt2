using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Semesterprojekt2.Service.BookATimeService;

namespace Semesterprojekt2.Pages.Orderhistory.BookATimeOrder
{
    public class AcceptOrderModel : PageModel
    {
        private IBookATimeService _BookATimeService;

        [BindProperty]
        public Models.BookATime.BookATime BookATime { get; set; }

        public AcceptOrderModel(IBookATimeService bookATimeService)
        {
            _BookATimeService = bookATimeService;
        }
        public IActionResult OnGet(int id)
        {
            BookATime = _BookATimeService.GetBookATimeById(id);
            if (BookATime == null)
                return RedirectToPage("/Error/Error"); //NotFound er ikke defineret endnu

            return Page();
        }
      
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            _BookATimeService.UpdateStatuesToAccept(BookATime);
            return RedirectToPage("/Orderhistory/BookATimeOverview");

        }
    }
}
