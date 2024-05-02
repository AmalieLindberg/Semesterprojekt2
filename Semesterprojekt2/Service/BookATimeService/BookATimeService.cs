using Semesterprojekt2.Models.BookATime;

namespace Semesterprojekt2.Service.BookATimeService
{
    public class BookATimeService : IBookATimeService
    {
        private BookATimeDbService _bookATimeDbService {  get; set; }

        public BookATimeService()
        {
            //BookATimeList = _bookATimeDbService.GetObjectsAsync().Result.ToList();
        }
        public List<BookATime> BookATimeList { get; set; }

        public async Task addTidsbestilling(BookATime bookATime)
        {
            BookATimeList.Add(bookATime);
            //await _bookATimeDbService.AddObjectAsync(bookATime);

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

        //GetUpdatedAvailableTimes matcher sammen med GetBookedTimesForDate og GetAvailableTimes
        //Bruger databasene
        //public async Task<List<string>> GetUpdatedAvailableTimes(DateTime date, List<string> additionalBookedTimes)
        //{
        //    List<string> bookedTimesFromDb = await _bookATimeDbService.GetBookedTimesForDate(date);

        //    // Kombiner bookede tider fra databasen med den ekstra liste
        //    var allBookedTimes = bookedTimesFromDb.Concat(additionalBookedTimes).Distinct().ToList();

        //    // Få den opdaterede liste over tilgængelige tider ved at passere alle bookede tider
        //    List<string> availableTimes = GetAvailableTimes(date, allBookedTimes);

        //    return availableTimes;
        //}
        public List<string> GetUpdatedAvailableTimes(DateTime date, List<string> additionalBookedTimes)
        {

            List<BookATime> bookedTimesFromDb = GetBookATimes();

            // Konverter bookedTimesFromDb til en liste af strengrepræsentationer af tidspunkter
            List<string> bookedTimes = bookedTimesFromDb
                                        .Where(t => t.DateForBooking == date.Date)
                                        .Select(t => t.DateForBooking.ToString("HH:mm")) // Formatet skal matche det i GetAvailableTimes
                                        .Distinct() // Undgå duplikater
                                        .ToList();

            // Kombiner de bookede tider med yderligere bookede tider
            var allBookedTimes = bookedTimes.Concat(additionalBookedTimes).Distinct().ToList();

            // Antag at GetAvailableTimes nu er en ikke-asynkron metode
            List<string> availableTimes = GetAvailableTimes(date, allBookedTimes);

            return availableTimes;
        }

        //GetBookedTimesForDate kun når man booker sætte ind i en list
        //Bruger database til det
        //public static async Task<List<string>> GetBookedTimesForDate(DateTime date)
        //{
        //    using (var context = new ItemDbContext())
        //    {
        //        return await context.Tidsbestillings
        //                            .Where(t => t.Dato.Date == date.Date)
        //                            .Select(t => t.Dato.ToString("HH:mm")) // Sørg for at formatet her matcher det i GetAvailableTimes
        //                            .Distinct() // Tilføj Distinct for at undgå duplikater
        //                            .ToListAsync();
        //    }
        //}
        //GetAvailableTimes List af tider som er muligt og hvilket dag

        public static List<string> GetAvailableTimes(DateTime date, List<string> bookedTimes)
        {
            List<string> potentialTimes = new List<string>();

            switch (date.DayOfWeek)
            {
                case DayOfWeek.Saturday:
                case DayOfWeek.Sunday:
                case DayOfWeek.Monday:
                case DayOfWeek.Tuesday:
                    potentialTimes.AddRange(new List<string> { "10:00", "11:00", "12:00" });
                    // Kontroller om nogen af tiderne er booket, og fjern alle hvis sandt.
                    if (potentialTimes.Any(pt => bookedTimes.Contains(pt)))
                    {
                        return new List<string>(); // Returnerer en tom liste, hvis en tid er booket.
                    }
                    break;

                case DayOfWeek.Wednesday:
                case DayOfWeek.Thursday:
                case DayOfWeek.Friday:
                    potentialTimes.AddRange(new List<string> { "09:00", "13:00" });
                    // Fjern kun de tider der faktisk er bookede.
                    potentialTimes = potentialTimes.Except(bookedTimes).ToList();
                    break;

                default:
                    return new List<string>(); // Returnerer en tom liste, hvis ugyldig dag.
            }

            return potentialTimes; // Returner opdaterede tilgængelige tider.
        }
        //GetDayClass matcher sammen med GetUpdatedAvailableTimes så hvis listen er =0 så røde ellers dagene oragne og gul
        public string GetDayClass(DateTime date, List<string> additionalBookedTimes)
        {
         
            // Få opdateret liste af tilgængelige tider for den givne dato
            var availableTimes = GetUpdatedAvailableTimes(date, additionalBookedTimes);

            // Tjek om der ikke er nogen tilgængelige tider
            if (availableTimes.Count == 0)
            {
                return "red-day";
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
