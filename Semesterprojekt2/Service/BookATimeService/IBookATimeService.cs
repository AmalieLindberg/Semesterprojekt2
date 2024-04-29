using Semesterprojekt2.Models.BookATime;

namespace Semesterprojekt2.Service.BookATimeService
{
    public interface IBookATimeService
    { 
        Task addTidsbestilling(BookATime bookATime);

        (int Year, int Month) AdjustYearAndMonth(int year, int month);

        List<string> GetUpdatedAvailableTimes(DateTime date, List<string> additionalBookedTimes);

       string GetDayClass(DateTime date, List<string> additionalBookedTimes);
       
    }
}

