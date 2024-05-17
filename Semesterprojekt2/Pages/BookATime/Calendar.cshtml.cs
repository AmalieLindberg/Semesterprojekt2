using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Semesterprojekt2.Models;
using Semesterprojekt2.Pages.Login;
using Semesterprojekt2.Service.BookATimeService;
using Semesterprojekt2.Service.UserService.UserService;

namespace Semesterprojekt2.Pages.BookATime
{
    public class CalendarModel : PageModel
    {
      
     public CalendarModel(IBookATimeService tidsbestillingService, BookedDaysService bookedDays, UserService userService)
        {
            _bookATimeService = tidsbestillingService;
            this.bookedDays = bookedDays;
            _userService = userService;
        }
        private IBookATimeService _bookATimeService;
        private BookedDaysService bookedDays;
        private UserService _userService;

        public List<int> DaysInMonth { get; set; }
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

        public Dictionary<DateTime, string> DayClasses { get; set; } = new Dictionary<DateTime, string>();
        public Dictionary<DateTime, bool> IsPastDict { get; set; } = new Dictionary<DateTime, bool>();
        public bool IsPast(DateTime date)
        {
            return date.Date < DateTime.Today;
        }

        public IEnumerable<Models.Dog> MyDogs { get; set; }
        public async Task OnGetAsync()
        {
           
          
            (Year, Month) = _bookATimeService.AdjustYearAndMonth(Year, Month);

            DaysInMonth = Enumerable.Range(1, DateTime.DaysInMonth(Year, Month)).ToList();

            //new DateTime(Year, Month, 1) skaber et nyt DateTime objekt for den f�rste dag i den givne m�ned og �r.
            //.ToString("MMMM") konverterer denne dato til en streng, der repr�senterer m�nedens fulde navn p� det aktuelle sprog af systemets lokalindstillinger.
            MonthName = new DateTime(Year, Month, 1).ToString("MMMM");

            await InitializeCalendar(Year, Month);

        }
        
        private async Task InitializeCalendar(int year, int month)
        {
            int daysInMonth = DateTime.DaysInMonth(year, month);
            // Fetch the list of booked periods from the service
            

            for (int day = 1; day <= daysInMonth; day++)
            {
                var date = new DateTime(year, month, day);
                var additionalBookedTimes = new List<string>();  // Using an empty list for simplicity

                // First, determine if the current date is in the past
                bool isPast = IsPast(date);
                IsPastDict[date] = isPast;  // Store past status in dictionary

                var availableTimes = await _bookATimeService.GetUpdatedAvailableTimes(date, additionalBookedTimes);
                var dayClass = await _bookATimeService.GetColurDay(date, additionalBookedTimes);
                AvailableTimesDict[date] = availableTimes;
                DayClasses[date] = dayClass;
            }
         }

        public IActionResult OnPost(int year, int month, int day, string tidspunkt)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (LoginModel.LoggedInUser == null || LoginModel.LoggedInUser.UserId == 0 || LoginModel.LoggedInUser.UserId == null)
            {
                return RedirectToPage("/Login/Login");
            }
            int id = LoginModel.LoggedInUser.UserId;
            Users CurrentUser = _userService.GetUser(id);
            MyDogs = _userService.GetUserDogs(CurrentUser).Dog;
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
            if (MyDogs == null || !MyDogs.Any())
            {
                return RedirectToPage("/BookATime/BookATimeWithoutDog");
            }
            // Redirect til en ny side uden at eksponere v�rdierne i URL'en
            return RedirectToPage("/BookATime/BookATime");


        }
    }
}
