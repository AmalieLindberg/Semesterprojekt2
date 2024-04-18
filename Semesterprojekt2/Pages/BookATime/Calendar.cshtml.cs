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
            //N�r du s�tter SupportsGet = true, fort�ller man ASP.NET Core,
            //at det er sikkert at binde v�rdier fra foresp�rgselsstrengen til egenskaben under GET foresp�rgsler.
            //Dette er nyttigt i scenarier, hvor du �nsker at opretholde eller pr�-indstille data gennem en URL, s�som filtrering,
            //sortering eller vedligeholdelse af tilstande over flere foresp�rgsler.
            [BindProperty(SupportsGet = true)]
            public int Month { get; set; }
            public List<int> DaysInMonth { get; set; }
            //public List<(int Day, BookingStatus Status)> DaysInTheMonth { get; set; }





            public void OnGet()
            {
                (Year, Month) = _tidsbestillingService.AdjustYearAndMonth(Year, Month);

                DaysInMonth = Enumerable.Range(1, DateTime.DaysInMonth(Year, Month)).ToList();

                //new DateTime(Year, Month, 1) skaber et nyt DateTime objekt for den f�rste dag i den givne m�ned og �r.
                //.ToString("MMMM") konverterer denne dato til en streng, der repr�senterer m�nedens fulde navn p� det aktuelle sprog af systemets lokalindstillinger.
                MonthName = new DateTime(Year, Month, 1).ToString("MMMM");



            }
        
    }
}
