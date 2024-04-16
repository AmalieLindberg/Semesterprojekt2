using Semesterprojekt2.Models;

namespace Semesterprojekt2.Service
{
    public class TidsbestillingService
    {
      
        public TidsbestillingService()
        {
            
        }
        public List<Tidsbestilling> Tidsbestillings { get; set; }

        public async Task addTidsbestilling(Tidsbestilling tidsbestilling)
        {
            Tidsbestillings.Add(tidsbestilling);
            

        }

        public static DayOfWeek GetDayOfWeek(int year, int month, int day)
        {
            DateTime date = new DateTime(year, month, day);
            return date.DayOfWeek;
        }
        public static string GetDayClass(DateTime date)
        {
            switch (date.DayOfWeek)
            {
                case DayOfWeek.Saturday:
                case DayOfWeek.Sunday:
                case DayOfWeek.Monday:
                case DayOfWeek.Tuesday:
                    return "yellow-day";
                case DayOfWeek.Wednesday:
                case DayOfWeek.Thursday:
                case DayOfWeek.Friday:
                    return "orange-day";
                default:
                    return string.Empty; // Eller en standard klasse, hvis du foretrækker
            }

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
    }
}
