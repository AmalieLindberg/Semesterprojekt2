using Semesterprojekt2.Models.BookATime;

namespace Semesterprojekt2.Service.BookATimeService
{
    public class BookATimeService : IBookATimeService
    {
        private BookATimeDbService _bookATimeDbService {  get; set; }

        public BookATimeService(BookATimeDbService bookATimeDbService)
        {
            _bookATimeDbService = bookATimeDbService;
            BookATimeList = _bookATimeDbService.GetObjectsAsync().Result.ToList();
        }
        public List<BookATime> BookATimeList { get; set; }

        public async Task addTidsbestilling(BookATime bookATime)
        {
            BookATimeList.Add(bookATime);
            await _bookATimeDbService.AddObjectAsync(bookATime);

        }
        public List<BookATime> GetBookATimes()
        { return BookATimeList ?? new List<BookATime>(); ; }


      

        public (int Year, int Month) AdjustYearAndMonth(int year, int month)
        {
            if (year == 0 || month == 0)
            {
                year = DateTime.Now.Year;
                month = DateTime.Now.Month;
            }
            else
            {
                if (month < 1)
                {
                    month = 12;
                    year -= 1;
                }
                else if (month > 12)
                {
                    month = 1;
                    year += 1;
                }
            }
            return (year, month);
        }

        //Metoder 

        public async Task<List<string>> GetUpdatedAvailableTimes(DateTime date, List<string> additionalBookedTimes)
        {

            List<string> bookedTimesFromDb = await BookATimeDbService.GetBookedTimesForDate(date);

            // Kombiner bookede tider fra databasen med den ekstra liste
            var allBookedTimes = bookedTimesFromDb.Concat(additionalBookedTimes).Distinct().ToList();

            // Få den opdaterede liste over tilgængelige tider ved at passere alle bookede tider
            List<string> availableTimes = GetAvailableTimes(date, allBookedTimes);

            return availableTimes;
        }

        public List<string> GetPotentialTimes(DateTime date)
        {
            List<string> potentialTimes = new List<string>();

            switch (date.DayOfWeek)
            {
                case DayOfWeek.Saturday:
                case DayOfWeek.Sunday:
                case DayOfWeek.Monday:
                case DayOfWeek.Tuesday:
                    potentialTimes.AddRange(new List<string> { "10:00", "11:00", "12:00" });
                    break;

                case DayOfWeek.Wednesday:
                case DayOfWeek.Thursday:
                case DayOfWeek.Friday:
                    break;

                default:
                    return new List<string>(); // Returnerer en tom liste, hvis ugyldig dag.
            }
            return potentialTimes;
        }
        
        public static List<string> GetServiceTimes(DayOfWeek day)
        {
            switch (day)
            {
                case DayOfWeek.Saturday:
                case DayOfWeek.Sunday:
                case DayOfWeek.Monday:
                case DayOfWeek.Tuesday:
                    return new List<string> { "10:00", "11:00", "12:00" }; // Mobile service times
                case DayOfWeek.Wednesday:
                case DayOfWeek.Thursday:
                case DayOfWeek.Friday:
                    return new List<string> { "09:00", "13:00" }; // Salon service times
                default:
                    return new List<string>(); // No services available
            }
        }
   


        public static List<string> GetAvailableTimes(DateTime date, List<string> bookedTimes)
        {
            bool isMobileServiceDay = date.DayOfWeek == DayOfWeek.Saturday ||
                                      date.DayOfWeek == DayOfWeek.Sunday ||
                                      date.DayOfWeek == DayOfWeek.Monday ||
                                      date.DayOfWeek == DayOfWeek.Tuesday;

            List<string> serviceTimes = GetServiceTimes(date.DayOfWeek);  // Retrieve potential service times based on the day
            List<string> blockedTimes = BookedDaysService.GetBlockedTimesForDate(date); // Retrieve blocked times based on holidays

            // If it's a mobile service day and there's any booking, block the whole day
            if (isMobileServiceDay && bookedTimes.Any())
            {
                return new List<string>();  // Return an empty list to indicate no availability
            }

            // Otherwise, remove blocked and booked times from the available service times
            return serviceTimes.Where(time => !blockedTimes.Contains(time) && !bookedTimes.Contains(time)).ToList();
        }

        //GetDayClass matcher sammen med GetUpdatedAvailableTimes så hvis listen er =0 så røde ellers dagene oragne og gul

        public async Task<string> GetDayClass(DateTime date, List<string> additionalBookedTimes)
        {
            DateTime today = DateTime.Today;
            // Få opdateret liste af tilgængelige tider for den givne dato
            var availableTimes = await GetUpdatedAvailableTimes(date, additionalBookedTimes);

            if (date < today)
            {
                return "past-day";
            }

            // Tjek om der ikke er nogen tilgængelige tider
            if (availableTimes.Count == 0)
            {
                return "red";
            }

            // Definer hvilke dage der skal returnere hvilken farveklasse
            switch (date.DayOfWeek)
            {
                case DayOfWeek.Wednesday:
                case DayOfWeek.Thursday:
                case DayOfWeek.Friday:
                    return "orange-day";
                case DayOfWeek.Saturday:
                case DayOfWeek.Sunday:
                case DayOfWeek.Monday:
                case DayOfWeek.Tuesday:
                    return "yellow-day";
                default:
                    throw new InvalidOperationException("Ugyldig dag i ugen");
            }
        }
    }
}
