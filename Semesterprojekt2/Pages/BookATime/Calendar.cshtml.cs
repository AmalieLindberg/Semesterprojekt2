using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Semesterprojekt2.Service.BookATimeService;

namespace Semesterprojekt2.Pages.BookATime
{
    public class CalendarModel : PageModel
    {
      
     public CalendarModel(IBookATimeService tidsbestillingService)
      {
            _bookATimeService = tidsbestillingService;
      }
        private IBookATimeService _bookATimeService { get; set; }
        public List<int> DaysInMonth { get; set; }
        ////public List<(int Day, BookingStatus Status)> DaysInTheMonth { get; set; }
        public string MonthName { get; set; }
        //N�r du s�tter SupportsGet = true, fort�ller man ASP.NET Core,
        //at det er sikkert at binde v�rdier fra foresp�rgselsstrengen til egenskaben under GET foresp�rgsler.
        //Dette er nyttigt i scenarier, hvor du �nsker at opretholde eller pr�-indstille data gennem en URL, s�som filtrering,
        //sortering eller vedligeholdelse af tilstande over flere foresp�rgsler.
        [BindProperty(SupportsGet = true)]
        public int Year { get; set; }
        [BindProperty(SupportsGet = true)]
        public int Month { get; set; }
        [BindProperty(SupportsGet = true)]
        public int Day { get; set; }
        [BindProperty]
        public string Tidspunkt { get; set; } // Dette vil blive en string repr�sentation af TimeSpan

        public Dictionary<DateTime, List<string>> AvailableTimesDict { get; set; } = new Dictionary<DateTime, List<string>>();
        public DateTime CurrentDate { get; set; }
        public Dictionary<DateTime, string> DayClasses { get; set; } = new Dictionary<DateTime, string>();

        public void OnGet(int? year, int? month)
        {
            Year = Year > 0 ? Year : DateTime.Now.Year;
            Month = Month >= 1 && Month <= 12 ? Month : DateTime.Now.Month;
            CurrentDate = new DateTime(year ?? DateTime.Today.Year, month ?? DateTime.Today.Month, 1);

            for (int day = 1; day <= DateTime.DaysInMonth(CurrentDate.Year, CurrentDate.Month); day++)
            {
                var date = new DateTime(CurrentDate.Year, CurrentDate.Month, day);
                var additionalBookedTimes = new List<string>(); // Dette skal v�re dynamisk eller fra en kilde.
                var availableTimes = _bookATimeService.GetUpdatedAvailableTimes(date, additionalBookedTimes);
                AvailableTimesDict[date] = availableTimes;
                DayClasses[date] = _bookATimeService.GetDayClass(date, additionalBookedTimes);
            }


            (Year, Month) = _bookATimeService.AdjustYearAndMonth(Year, Month);

            DaysInMonth = Enumerable.Range(1, DateTime.DaysInMonth(Year, Month)).ToList();

            //new DateTime(Year, Month, 1) skaber et nyt DateTime objekt for den f�rste dag i den givne m�ned og �r.
            //.ToString("MMMM") konverterer denne dato til en streng, der repr�senterer m�nedens fulde navn p� det aktuelle sprog af systemets lokalindstillinger.
            MonthName = new DateTime(Year, Month, 1).ToString("MMMM");
          



        }

        public IActionResult OnPost(int year, int month, int day, string tidspunkt)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            // Opretter DateTime objektet fra de indsendte formularv�rdier
            //Denne linje parser en streng repr�sentation af et tidspunkt (tidspunkt) til en TimeSpan objekt. Det antages her,
            //at tidspunkt er en gyldig streng, der repr�senterer en tidsperiode (f.eks. "14:30"). Parsing fejler med en exception,
            //hvis strengen ikke er gyldig.
            //TimeSpan indeholder kun tidsdelen, dvs. timer og minutter i dette tilf�lde.
            TimeSpan time = TimeSpan.Parse(tidspunkt); // Antagelse: 'tidspunkt' er en gyldig TimeSpan string
            //Her oprettes et DateTime objekt ved at specificere �r (year), m�ned (month), dag (day),
            //og tidsdelen fra TimeSpan objektet (time.Hours og time.Minutes).
            //Sekunderne er sat til 0. Denne metode bruges til pr�cist at oprette et DateTime objekt med b�de dato og pr�cis tid.
            DateTime fullDateTime = new DateTime(year, month, day, time.Hours, time.Minutes, 0);
            //fullDateTime objektet gemmes i TempData under n�glen "FullDateTime". TempData er en funktion i ASP.NET Core,
            //der anvendes til at gemme data mellem HTTP-anmodninger i et kort tidsinterval. Dette er is�r nyttigt i scenarier,
            //hvor du skal bevare v�rdier mellem forskellige sider eller handlinger i en applikation, indtil de senere skal anvendes.
            // Gem v�rdierne midlertidigt
            TempData["FullDateTime"] = fullDateTime;

            // Redirect til en ny side uden at eksponere v�rdierne i URL'en
            return RedirectToPage("/BookATime/BookATime");


        }
    }
}
