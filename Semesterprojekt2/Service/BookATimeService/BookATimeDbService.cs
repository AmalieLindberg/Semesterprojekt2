using Microsoft.EntityFrameworkCore;
using Semesterprojekt2.EFDbContext;
using Semesterprojekt2.Models;
using Semesterprojekt2.Models.BookATime;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Semesterprojekt2.Service.BookATimeService
{
    public class BookATimeDbService : DbGenericService<BookATime>
    {
        

        public async Task<List<string>> GetBookedTimesForDate(DateTime date)
        {
            using (var context = new SemsterprojektDbContext())
            {
                return await context.BookATime //context.BookATime: Refererer til BookATime tabellen i databasen.
                                    .Where(t => t.DateForBooking.Date == date.Date) //.Where(t => t.DateForBooking.Date == date.Date): Filtrerer rækkerne,
                                                                                    //så kun de rækker, hvor DateForBooking har samme dato som date, inkluderes.
                                                                                    // t.DateForBooking.Date: Ekstraherer kun dato-delen af DateForBooking (ignorerer tiden).
                                                                                    // date.Date: Ekstraherer kun dato - delen af date(ignorerer tiden).

                                    .Select(t => t.DateForBooking.ToString("HH:mm")) //.Select(t => t.DateForBooking.ToString("HH:mm")):
                                                                                     //Projekterer hver række til en streng, der repræsenterer tiden i formatet "HH:mm".
                                                                                     //t.DateForBooking.ToString("HH:mm"): Konverterer DateForBooking til en streng, der kun indeholder time og minut.
                                                                                     // Sørg for at formatet her matcher det i GetAvailableTimes

                                    .Distinct() // Distinct gør at man undgå duplikater
                                    .ToListAsync(); //Konverterer det hele til en liste og udfører forespørgslen asynkront.
            }
        }

       
    }
}
