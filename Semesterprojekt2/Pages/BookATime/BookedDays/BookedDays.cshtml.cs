using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Semesterprojekt2.Models.BookATime;
using Semesterprojekt2.Service;
using Semesterprojekt2.Service.BookATimeService;
using Semesterprojekt2.Service.UserService.UserService;
using System.Collections;

namespace Semesterprojekt2.Pages.BookATime.BookedDays
{   [Authorize(Roles = "Admin")]
    public class BookedDaysModel : PageModel
    {
     
        public BookedDaysModel(BookedDaysService bookedDaysService, IUserService userService)
        {
            _bookedDaysService = bookedDaysService;
            _userService = userService;
        }

        public List<Models.BookATime.BookedDays> BookedDays { get; set; }
        public BookedDaysService _bookedDaysService;
        private IUserService _userService;
        public void OnGet()
        {
            BookedDays = _bookedDaysService.GetBookedDaysList();

            foreach (var booked in BookedDays)
            {
                if (booked != null && booked.UserId != null) // Tjekker at booked og UserId ikke er null
                {
                    booked.User = _userService.GetUser(booked.UserId);
                }
            }

        }
        public IActionResult OnGetSortById()
        {
            BookedDays = _bookedDaysService.SortById().ToList();
            return Page();
        }
        public IActionResult OnGetSortByIdDescending()
        {
            BookedDays = _bookedDaysService.SortByIdDescending().ToList();
            return Page();
        }
        public IActionResult OnGetSortByStartDate()
        {
            BookedDays = _bookedDaysService.SortByStartDate().ToList();
            return Page();
        }
        public IActionResult OnGetSortByStartDateDescending()
        {
            BookedDays = _bookedDaysService.SortByStartDateDescending().ToList();
            return Page();
        }
        public IActionResult OnGetSortByEndDate()
        {
            BookedDays = _bookedDaysService.SortByEndDate().ToList();
            return Page();
        }
        public IActionResult OnGetSortByEndDateDescending()
        {
            BookedDays = _bookedDaysService.SortByEndDateDescending().ToList();
            return Page();
        }
    }
}
