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
        //Når du sætter SupportsGet = true, fortæller man ASP.NET Core,
        //at det er sikkert at binde værdier fra forespørgselsstrengen til egenskaben under GET forespørgsler.
        //Dette er nyttigt i scenarier, hvor du ønsker at opretholde eller præ-indstille data gennem en URL, såsom filtrering,
        //sortering eller vedligeholdelse af tilstande over flere forespørgsler.
        [BindProperty(SupportsGet = true)]
        public int Year { get; set; }
        [BindProperty(SupportsGet = true)]
        public int Month { get; set; }
        [BindProperty(SupportsGet = true)]
        public int Day { get; set; }
        [BindProperty]
        public string Tidspunkt { get; set; } // Dette vil blive en string repræsentation af TimeSpan

        public Dictionary<DateTime, List<string>> AvailableTimesDict { get; set; } = new Dictionary<DateTime, List<string>>();

        public Dictionary<DateTime, string> DayClasses { get; set; } = new Dictionary<DateTime, string>();
        public Dictionary<DateTime, bool> IsPastDict { get; set; } = new Dictionary<DateTime, bool>();

        //Den sammenligner datoen uden tidspunkt (date.Date) med dagens dato (DateTime.Today).
        //Metoden returnerer true hvis datoen er i fortiden, ellers false. 
        public bool IsPast(DateTime date)
        {
            return date.Date < DateTime.Today;
        }

        public IEnumerable<Models.Dog> MyDogs { get; set; }
        public async Task OnGetAsync()
        {
            //(Year, Month) = _bookATimeService.AdjustYearAndMonth(Year, Month); 
            //bruges til at justere år og måned måske baseret på input fra brugeren eller systemlogik,
            //vilket sikrer at de er gyldige før de anvendes til at oprette kalenderen.
            (Year, Month) = _bookATimeService.AdjustYearAndMonth(Year, Month);
            //DaysInMonth = Enumerable.Range(1, DateTime.DaysInMonth(Year, Month)).ToList();
            //opretter en liste af dage i den specificerede måned og år.Enumerable.Range genererer en sekvens af tal(dage), 
            //hvor DateTime.DaysInMonth angiver antallet af dage i den pågældende måned.
            DaysInMonth = Enumerable.Range(1, DateTime.DaysInMonth(Year, Month)).ToList();

            //new DateTime(Year, Month, 1) skaber et nyt DateTime objekt for den første dag i den givne måned og år.
            //.ToString("MMMM") konverterer denne dato til en streng, der repræsenterer månedens fulde navn på det aktuelle sprog af systemets lokalindstillinger.
            MonthName = new DateTime(Year, Month, 1).ToString("MMMM");
            //await InitializeCalendar(Year, Month); kalder en anden metode InitializeCalendar asynkront, 
            //som opbygger selve kalenderdatastrukturen eller - visningen baseret på det justerede år og måned.
            await InitializeCalendar(Year, Month);

        }
        
        private async Task InitializeCalendar(int year, int month)
        {
            //int daysInMonth = DateTime.DaysInMonth(year, month); 
            //beregner antallet af dage i den specificerede måned og år,
            //som er nødvendigt for at iterere over hver dag i måneden.

            int daysInMonth = DateTime.DaysInMonth(year, month);


            for (int day = 1; day <= daysInMonth; day++)
            {
                //skaber et DateTime objekt for hver dag i måneden bruger sammen day som i for.
                var date = new DateTime(year, month, day);

                // Using an empty list for simplicity
                var additionalBookedTimes = new List<string>(); 

                //tjekker om datoen er i fortiden ved hjælp af metoden IsPast.
                bool isPast = IsPast(date);
                //gemmer resultatet i et dictionary for hurtig adgang senere.
                IsPastDict[date] = isPast;  // Store past status in dictionary
                //henter opdaterede tilgængelige tider for den givne dag asynkront.
                var availableTimes = await _bookATimeService.GetUpdatedAvailableTimes(date, additionalBookedTimes);
                //henter en farverne på dagene.
                var dayClass = await _bookATimeService.GetColurDay(date, additionalBookedTimes);
                //gemmer resultatet i et dictionary for hurtig adgang senere.
                AvailableTimesDict[date] = availableTimes;
                //gemmer resultatet i et dictionary for hurtig adgang senere.
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
            // Opretter DateTime objektet fra de indsendte formularværdier
            //Denne linje parser en streng repræsentation af et tidspunkt (tidspunkt) til en TimeSpan objekt. Det antages her,
            //at tidspunkt er en gyldig streng, der repræsenterer en tidsperiode (f.eks. "14:30"). Parsing fejler med en exception,
            //hvis strengen ikke er gyldig.
            //TimeSpan indeholder kun tidsdelen, dvs. timer og minutter i dette tilfælde.

            TimeSpan time = TimeSpan.Parse(tidspunkt); // Antagelse: 'tidspunkt' er en gyldig TimeSpan string
            //Her oprettes et DateTime objekt ved at specificere år (year), måned (month), dag (day),
            //og tidsdelen fra TimeSpan objektet (time.Hours og time.Minutes).
            //Sekunderne er sat til 0. Denne metode bruges til præcist at oprette et DateTime objekt med både dato og præcis tid.


            DateTime fullDateTime = new DateTime(year, month, day, time.Hours, time.Minutes, 0);



            //fullDateTime objektet gemmes i TempData under nøglen "FullDateTime". TempData er en funktion i ASP.NET Core,
            //der anvendes til at gemme data mellem HTTP-anmodninger i et kort tidsinterval. Dette er især nyttigt i scenarier,
            //hvor du skal bevare værdier mellem forskellige sider eller handlinger i en applikation, indtil de senere skal anvendes.
            // Gem værdierne midlertidigt

            TempData["FullDateTime"] = fullDateTime;

            //Hvis hunden list for user er null eller ikke har nogen så sendes de over til BookATimeWithoutDog
            if (MyDogs == null || !MyDogs.Any())
            {
                return RedirectToPage("/BookATime/BookATimeWithoutDog");
            }
         //ellers sendes de over til bookatime
            return RedirectToPage("/BookATime/BookATime");


        }
    }
}
