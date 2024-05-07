using Microsoft.EntityFrameworkCore;
using Semesterprojekt2.EFDbContext;
using Semesterprojekt2.Models.BookATime;

namespace Semesterprojekt2.Service.BookATimeService
{
    public class BookATimeDbService : DbGenericService<BookATime>
    {
        public static async Task<List<string>> GetBookedTimesForDate(DateTime date)
        {
            using (var context = new SemsterprojektDbContext())
            {
                return await context.BookATime
                                    .Where(t => t.DateForBooking.Date == date.Date)
                                    .Select(t => t.DateForBooking.ToString("HH:mm")) // Sørg for at formatet her matcher det i GetAvailableTimes
                                    .Distinct() // Tilføj Distinct for at undgå duplikater
                                    .ToListAsync();
            }
        }
    }
}
