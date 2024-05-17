using Semesterprojekt2.Models.BookATime;

namespace Semesterprojekt2.Service.BookATimeService
{
    public class BookedDaysService
    {
        private DbGenericService<BookedDays> _BookedDaysDbService { get; set; }
        public static List<BookedDays> _bookedDays { get; set; }

        public BookedDaysService(DbGenericService<BookedDays> bookedDaysDbService)
        {
            _BookedDaysDbService = bookedDaysDbService;
            
            _bookedDays = _BookedDaysDbService.GetObjectsAsync().Result.ToList();
        }

        public async Task AddBookedDays(BookedDays dBookedDays)
        {
            _bookedDays.Add(dBookedDays);
            await _BookedDaysDbService.AddObjectAsync(dBookedDays);
        }
        //public async Task<List<BookedDays>> GetBookedDays(int year, int month)
        //{
        //    // Using Task.FromResult to simulate asynchronous operation with static data
        //    return await Task.FromResult(_bookedDays.Where(p =>
        //        (p.StartDate.Year == year && p.StartDate.Month == month) || (p.EndDate.Year == year && p.EndDate.Month == month)).ToList());
        //}
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
           
            return _bookedDays;
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
            foreach (var booked in _bookedDays)
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
        //private static List<string> BlockWholeDay(DateTime date)
        //{
        //    List<string> times = new List<string>();
        //    for (int hour = 0; hour < 24; hour++)
        //    {
        //        times.Add(date.Date.AddHours(hour).ToString("HH:mm"));
        //    }
        //    return times;
        //}

        //private static List<string> BlockSingleDay(BookedDays bookedDays)
        //{
        //    List<string> times = new List<string>();
        //    DateTime time = new DateTime(bookedDays.StartDate.Year, bookedDays.StartDate.Month, bookedDays.StartDate.Day, bookedDays.StartDate.Hour, 0, 0);
        //    while (time <= bookedDays.EndDate)
        //    {
        //        times.Add(time.ToString("HH:mm"));
        //        time = time.AddHours(1);
        //    }
        //    return times;
        //}

        //private static List<string> BlockStartingDay(BookedDays bookedDays, DateTime date)
        //{
        //    List<string> times = new List<string>();
        //    DateTime time = new DateTime(bookedDays.StartDate.Year, bookedDays.StartDate.Month, bookedDays.StartDate.Day, bookedDays.StartDate.Hour, 0, 0);
        //    while (time < date.Date.AddDays(1))  // Block until the end of the day
        //    {
        //        times.Add(time.ToString("HH:mm"));
        //        time = time.AddHours(1);
        //    }
        //    return times;
        //}

        //private static List<string> BlockEndingDay(BookedDays bookedDays, DateTime date)
        //{
        //    List<string> times = new List<string>();
        //    DateTime time = date.Date;  // Start of the day
        //    while (time <= bookedDays.EndDate)
        //    {
        //        times.Add(time.ToString("HH:mm"));
        //        time = time.AddHours(1);
        //    }
        //    return times;
        //}

        // Block the entire day by adding each hour to the list.
        private static List<string> BlockWholeDay(DateTime date)
        {
            return Enumerable.Range(0, 24).Select(hour => date.Date.AddHours(hour).ToString("HH:mm")).ToList();
        }

        // Block specific hours during a single day.
        private static List<string> BlockSingleDay(BookedDays bookedDays)
        {
            List<string> times = new List<string>();
            DateTime time = new DateTime(bookedDays.StartDate.Year, bookedDays.StartDate.Month, bookedDays.StartDate.Day,
                                         bookedDays.StartDate.Hour, 0, 0);
            while (time <= bookedDays.EndDate)
            {
                times.Add(time.ToString("HH:mm"));
                time = time.AddHours(1);
            }
            return times;
        }

        // Block from the start of a booking to the end of the day.
        private static List<string> BlockStartingDay(BookedDays bookedDays, DateTime date)
        {
            List<string> times = new List<string>();
            DateTime time = new DateTime(bookedDays.StartDate.Year, bookedDays.StartDate.Month, bookedDays.StartDate.Day,
                                         bookedDays.StartDate.Hour, 0, 0);
            while (time < date.Date.AddDays(1))
            {
                times.Add(time.ToString("HH:mm"));
                time = time.AddHours(1);
            }
            return times;
        }

        // Block from the start of the day to the end time of a booking.
        private static List<string> BlockEndingDay(BookedDays bookedDays, DateTime date)
        {
            List<string> times = new List<string>();
            DateTime time = date.Date;
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
