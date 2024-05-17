using Semesterprojekt2.Models.BookATime;

namespace Semesterprojekt2.Service.BookATimeService
{
    public interface IBookATimeService
    { 
        Task addTidsbestilling(BookATime bookATime);

        (int Year, int Month) AdjustYearAndMonth(int year, int month);

        Task<List<string>> GetUpdatedAvailableTimes(DateTime date, List<string> additionalBookedTimes);

       Task<string> GetColurDay(DateTime date, List<string> additionalBookedTimes);
        IEnumerable<BookATime>? GetAllBookATimeList();
        BookATime GetBookATimeById(int id);
        Task<BookATime> DeleteBookATime(int id);
        Task<BookATime> UpdateStatuesToAccept(BookATime bookATime);
        Task<BookATime> UpdateBookATime(BookATime bookATime);
        Task<List<BookATime>> DeleteDogInBookATime(int dogid);
        Task<List<BookATime>> DeleteUserInBookATime(int userid);


    }
}

