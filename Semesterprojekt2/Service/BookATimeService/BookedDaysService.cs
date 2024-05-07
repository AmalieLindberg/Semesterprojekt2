using Semesterprojekt2.Models.BookATime;

namespace Semesterprojekt2.Service.BookATimeService
{
    public class BookedDaysService
    {
        private DbGenericService<BookedDays> _BookedDaysDbService { get; set; }
        private static List<BookedDays>? _bookedDays {  get; set; }

        public BookedDaysService(DbGenericService<BookedDays> bookedDaysDbService)
        {            
            _BookedDaysDbService = bookedDaysDbService;
            
           
            _bookedDays = _BookedDaysDbService.GetObjectsAsync().Result.ToList();
        }

        public async Task AddBookedDays(DateTime start, DateTime slut)
        {
            _bookedDays.Add(new BookedDays { StartDate = start, EndDate = slut });
            await _BookedDaysDbService.AddObjectAsync(new BookedDays { StartDate = start, EndDate = slut });
        }
        public async Task<BookedDays> RemoveBookedDaysById(int id)
        {
            foreach (var bookedDays in _bookedDays)
            {
                if (bookedDays.Id == id) {
                    _bookedDays.Remove(bookedDays);
                    await _BookedDaysDbService.DeleteObjectAsync(bookedDays); 
                    return bookedDays;
                }
            }
            return null;

        }

        public static List<BookedDays> GetBookedDaysList()
        {
            return _bookedDays ?? new List<BookedDays>();
        }
  
        

        public BookedDays GetBookedDaysById(int id)
        {
            foreach (var i in _bookedDays)
            {
                if (i.Id == id)
                    return i;
            }
            return null;
        }
        public static List<string> GetBlockedTimesForDate(DateTime date)
        {
            List<string> blockedTimes = new List<string>();

            foreach (var booked in GetBookedDaysList())
            {
                if (date.Date > booked.StartDate.Date && date.Date < booked.EndDate.Date)
                {
                    blockedTimes.AddRange(BlockWholeDay(date));
                }
                else if (date.Date == booked.StartDate.Date && date.Date == booked.EndDate.Date)
                {
                    blockedTimes.AddRange(BlockSingleDay(booked));
                }
                else if (date.Date == booked.StartDate.Date)
                {
                    blockedTimes.AddRange(BlockStartingDay(booked, date));
                }
                else if (date.Date == booked.EndDate.Date)
                {
                    blockedTimes.AddRange(BlockEndingDay(booked, date));
                }
            }

            return blockedTimes.Distinct().ToList();
        }
        private static List<string> BlockWholeDay(DateTime date)
        {
            List<string> times = new List<string>();
            for (int hour = 0; hour < 24; hour++)
            {
                times.Add(date.Date.AddHours(hour).ToString("HH:mm"));
            }
            return times;
        }

        private static List<string> BlockSingleDay(BookedDays bookedDays)
        {
            List<string> times = new List<string>();
            DateTime time = new DateTime(bookedDays.StartDate.Year, bookedDays.StartDate.Month, bookedDays.StartDate.Day, bookedDays.StartDate.Hour, 0, 0);
            while (time <= bookedDays.EndDate)
            {
                times.Add(time.ToString("HH:mm"));
                time = time.AddHours(1);
            }
            return times;
        }

        private static List<string> BlockStartingDay(BookedDays bookedDays, DateTime date)
        {
            List<string> times = new List<string>();
            DateTime time = new DateTime(bookedDays.StartDate.Year, bookedDays.StartDate.Month, bookedDays.StartDate.Day, bookedDays.StartDate.Hour, 0, 0);
            while (time < date.Date.AddDays(1))  // Block until the end of the day
            {
                times.Add(time.ToString("HH:mm"));
                time = time.AddHours(1);
            }
            return times;
        }

        private static List<string> BlockEndingDay(BookedDays bookedDays, DateTime date)
        {
            List<string> times = new List<string>();
            DateTime time = date.Date;  // Start of the day
            while (time <= bookedDays.EndDate)
            {
                times.Add(time.ToString("HH:mm"));
                time = time.AddHours(1);
            }
            return times;
        }



        public IEnumerable<BookedDays> SortById()
        {
            var ById = from BookedDays in _bookedDays
                       orderby BookedDays.Id
                       select BookedDays;
            return ById;
        }
        public IEnumerable<BookedDays> SortByIdDescending()
        {
            var ByIdDescending = from BookedDays in _bookedDays
                                 orderby BookedDays.Id descending
                                 select BookedDays;
            return ByIdDescending;
        }
        public IEnumerable<BookedDays> SortByStartDate()
        {
            var ById = from BookedDays in _bookedDays
                       orderby BookedDays.StartDate
                       select BookedDays;
            return ById;
        }
        public IEnumerable<BookedDays> SortByStartDateDescending()
        {
            var ByIdDescending = from BookedDays in _bookedDays
                                 orderby BookedDays.StartDate descending
                                 select BookedDays;
            return ByIdDescending;
        }
        public IEnumerable<BookedDays> SortByEndDate()
        {
            var ById = from BookedDays in _bookedDays
                       orderby BookedDays.EndDate
                       select BookedDays;
            return ById;
        }
        public IEnumerable<BookedDays> SortByEndDateDescending()
        {
            var ByIdDescending = from BookedDays in _bookedDays
                                 orderby BookedDays.EndDate descending
                                 select BookedDays;
            return ByIdDescending;
        }

    }
}
