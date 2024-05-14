using Microsoft.AspNetCore.Mvc;
using Semesterprojekt2.Models;
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
        //public async Task<List<string>> GetUpdatedAvailableTimes(DateTime date, List<string> additionalBookedTimes)
        //{
        //    // Retrieve booked times from the database
        //    List<string> bookedTimesFromDb = await BookATimeDbService.GetBookedTimesForDate(date);

        //    // Retrieve blocked times (e.g., due to holidays or special events)
        //    List<string> blockedTimes = BookedDaysService.GetBlockedTimesForDate(date);

        //    // Combine booked times from database, blocked times, and additional booked times
        //    var allBookedTimes = bookedTimesFromDb.Concat(blockedTimes).Concat(additionalBookedTimes).Distinct().ToList();

        //    // Get the updated list of available times by filtering out all booked times
        //    List<string> availableTimes = GetAvailableTimes(date, allBookedTimes);

        //    return availableTimes;
        //}




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

            // Proceed with calculating available times
            bool isMobileServiceDay = date.DayOfWeek == DayOfWeek.Saturday ||
                                      date.DayOfWeek == DayOfWeek.Sunday ||
                                      date.DayOfWeek == DayOfWeek.Monday ||
                                      date.DayOfWeek == DayOfWeek.Tuesday;

            List<string> serviceTimes = GetServiceTimes(date.DayOfWeek);
            List<string> blockedTimes = BookedDaysService.GetBlockedTimesForDate(date);
          
                if (isMobileServiceDay && bookedTimes.Any())
                {
                    return new List<string>();  // Return an empty list indicating no availability
                }
            
            return serviceTimes.Where(time => !blockedTimes.Contains(time) && !bookedTimes.Contains(time)).ToList();
        }



       
        //GetDayClass matcher sammen med GetUpdatedAvailableTimes så hvis listen er =0 så røde ellers dagene oragne og gul

        public async Task<string> GetColurDay(DateTime date, List<string> additionalBookedTimes)
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

        public IEnumerable<BookATime>? GetAllBookATimeList()
        {
            return BookATimeList;
        }

        public BookATime GetBookATimeById(int id)
        {
            foreach (var bookATime in BookATimeList)
            {
                if (id == bookATime.Id)
                    return bookATime;
            }
            return null;
        }

        public async Task<BookATime> DeleteBookATime(int id)
        {
            BookATime itemToBeDeleted = null;
            foreach (BookATime bookATime in BookATimeList)
            {
                if (bookATime.Id == id)
                {
                    itemToBeDeleted = bookATime;
                    break;
                }
            }
            if (itemToBeDeleted != null)
            {
                BookATimeList.Remove(itemToBeDeleted);
                
                await _bookATimeDbService.DeleteObjectAsync(itemToBeDeleted);
                return itemToBeDeleted;
            }
            return null;
        }

        public async Task<BookATime> UpdateStatuesToAccept(BookATime bookATime)
        {
            if (bookATime != null)
            {
                foreach (BookATime book in BookATimeList)
                {
                    if (book.Id == bookATime.Id)
                    {
                        book.StatusForBooking = "Accepted"; 
                        await _bookATimeDbService.UpdateObjectAsync(book);
                       return book;
                    }
                }
                
             
            }
            return null;
        }
        public async Task<BookATime> UpdateBookATime(BookATime bookATime)
        {
            if (bookATime != null)
            {
                foreach (BookATime book in BookATimeList)
                {
                    if (book.Id == bookATime.Id)
                    {
                        book.Address = bookATime.Address;
                        book.Comments = bookATime.Comments;
                        book.FirstTime = bookATime.FirstTime;
                        book.Floors = bookATime.Floors;
                        book.Elevator = bookATime.Elevator;
                        if (bookATime.BathRoomImage==null)
                        {
                            bookATime.BathRoomImage = book.BathRoomImage;

                        }
                        else 
                            book.BathRoomImage = bookATime.BathRoomImage;
                       
                        await _bookATimeDbService.UpdateObjectAsync(book);
                        return book;
                    }
                }


            }
            return null;
        }
    }
}
