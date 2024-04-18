using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Semesterprojekt2.Service;

namespace Semesterprojekt2.Pages.BookATime
{
    public class CalendarModel : PageModel
    {
      
            public CalendarModel(BookATimeService tidsbestillingService)
            {
                _tidsbestillingService = tidsbestillingService;
            }
            private BookATimeService _tidsbestillingService { get; set; }
            [BindProperty(SupportsGet = true)]
            public int Year { get; set; }
            public string MonthName { get; set; }
            //Når du sætter SupportsGet = true, fortæller man ASP.NET Core,
            //at det er sikkert at binde værdier fra forespørgselsstrengen til egenskaben under GET forespørgsler.
            //Dette er nyttigt i scenarier, hvor du ønsker at opretholde eller præ-indstille data gennem en URL, såsom filtrering,
            //sortering eller vedligeholdelse af tilstande over flere forespørgsler.
            [BindProperty(SupportsGet = true)]
            public int Month { get; set; }
            public List<int> DaysInMonth { get; set; }
            //public List<(int Day, BookingStatus Status)> DaysInTheMonth { get; set; }





            public void OnGet()
            {
                (Year, Month) = _tidsbestillingService.AdjustYearAndMonth(Year, Month);

                DaysInMonth = Enumerable.Range(1, DateTime.DaysInMonth(Year, Month)).ToList();

                //new DateTime(Year, Month, 1) skaber et nyt DateTime objekt for den første dag i den givne måned og år.
                //.ToString("MMMM") konverterer denne dato til en streng, der repræsenterer månedens fulde navn på det aktuelle sprog af systemets lokalindstillinger.
                MonthName = new DateTime(Year, Month, 1).ToString("MMMM");



            }
        
    }
}
