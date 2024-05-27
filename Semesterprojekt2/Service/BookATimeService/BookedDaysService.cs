using Semesterprojekt2.Models;
using Semesterprojekt2.Models.BookATime;
using System.Collections.Generic;
using System.Runtime.Intrinsics.X86;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

        public List<BookedDays> GetBookedDaysList()
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

        //public List<string> GetBlockedTimesForDate(DateTime date)
        //{
        //    List<string> blockedTimes = new List<string>();

        //    foreach (var holiday in _bookedDays)
        //    {
        //        // Check if the holiday covers the entire day
        //        if (date.Date > holiday.StartDate.Date && date.Date < holiday.EndDate.Date)
        //        {
        //            // Block the whole day if the date is in the middle of a holiday period
        //            for (int hour = 0; hour < 24; hour++)
        //            {
        //                blockedTimes.Add(date.Date.AddHours(hour).ToString("HH:mm"));
        //            }
        //        }
        //        else
        //        {
        //            // Handle holidays that start or end on the given date
        //            if (date.Date == holiday.StartDate.Date && date.Date == holiday.EndDate.Date)
        //            {
        //                // Holiday starts and ends on the same day
        //                DateTime time = new DateTime(holiday.StartDate.Year, holiday.StartDate.Month, holiday.StartDate.Day, holiday.StartDate.Hour, 0, 0);
        //                while (time <= holiday.EndDate)
        //                {
        //                    blockedTimes.Add(time.ToString("HH:mm"));
        //                    time = time.AddHours(1);
        //                }
        //            }
        //            else if (date.Date == holiday.StartDate.Date)
        //            {
        //                // Holiday starts on this date but does not end on the same day
        //                DateTime time = new DateTime(holiday.StartDate.Year, holiday.StartDate.Month, holiday.StartDate.Day, holiday.StartDate.Hour, 0, 0);
        //                while (time < date.Date.AddDays(1))  // Block until the end of the day
        //                {
        //                    blockedTimes.Add(time.ToString("HH:mm"));
        //                    time = time.AddHours(1);
        //                }
        //            }
        //            else if (date.Date == holiday.EndDate.Date)
        //            {
        //                // Holiday ends on this date
        //                DateTime time = date.Date;  // Start of the day
        //                while (time <= holiday.EndDate)
        //                {
        //                    blockedTimes.Add(time.ToString("HH:mm"));
        //                    time = time.AddHours(1);
        //                }
        //            }
        //        }
        //    }
        //    return blockedTimes.Distinct().ToList();
        //}

        public List<string> GetBlockedTimesForDate(DateTime date)
        { //Formål: Opretter en tom liste, der skal holde de blokerede tider.
           //Hvordan: Initialiserer en ny liste af typen string.
              List<string> blockedTimes = new List<string>();

            //Gennemløber hver booked periode i _bookedDays samlingen for at bestemme, om og hvordan date skal blokeres
            foreach (var booked in _bookedDays)
            {
                //Hvis date er inden for en booket periode (men ikke på start- eller slutdatoen),
                //tilføjes alle tider for hele dagen til blockedTimes.
                //Hvordan:
                //date.Date > booked.StartDate.Date && date.Date < booked.EndDate.Date: Tjekker om date er mellem start-og slutdatoen for den bookede periode.
                //blockedTimes.AddRange(BlockWholeDay(date)): Tilføjer alle blokerede tider for hele dagen til blockedTimes.
                if (date.Date > booked.StartDate.Date && date.Date < booked.EndDate.Date)
                {
                    blockedTimes.AddRange(BlockWholeDay(date));
                }
                //Hvis date er både start- og slutdatoen for en booket periode (dvs. en enkelt dag),
                //tilføjes de blokerede tider for den dag til blockedTimes.
                //Hvordan:
                //date.Date == booked.StartDate.Date && date.Date == booked.EndDate.Date:
                //Tjekker om date er både start- og slutdatoen for den bookede periode.
                //blockedTimes.AddRange(BlockSingleDay(booked)): Tilføjer alle blokerede tider for den enkelte dag til blockedTimes.
                else if (date.Date == booked.StartDate.Date && date.Date == booked.EndDate.Date)
                {
                    blockedTimes.AddRange(BlockSingleDay(booked));
                }
                //Hvis date er startdatoen for en booket periode, tilføjes de blokerede tider for den startende dag til blockedTimes.
                //Hvordan:
                //date.Date == booked.StartDate.Date: Tjekker om date er startdatoen for den bookede periode.
                //blockedTimes.AddRange(BlockStartingDay(booked, date)): Tilføjer alle blokerede tider for startdatoen til blockedTimes.
                else if (date.Date == booked.StartDate.Date)
                {
                    blockedTimes.AddRange(BlockStartingDay(booked, date));
                }
                //Hvis date er slutdatoen for en booket periode, tilføjes de blokerede tider for den afsluttende dag til blockedTimes.
                //Hvordan:
                //date.Date == booked.EndDate.Date: Tjekker om date er slutdatoen for den bookede periode.
                //blockedTimes.AddRange(BlockEndingDay(booked, date)): Tilføjer alle blokerede tider for slutdatoen til blockedTimes.
                else if (date.Date == booked.EndDate.Date)
                            {
                    blockedTimes.AddRange(BlockEndingDay(booked, date));
                }
            }
            //Fjerner eventuelle duplikater fra blockedTimes og konverterer det tilbage til en liste.
            //Hvordan:
            //blockedTimes.Distinct(): Distinct() Fjerner duplikater fra blockedTimes.
            //.ToList(): Konverterer det filtrerede resultat tilbage til en liste.
            return blockedTimes.Distinct().ToList();
        }

        // Block the entire day by adding each hour to the list.
        
        private static List<string> BlockWholeDay(DateTime date)
        {
            //Genererer en liste over alle tidspunkter(i timer) fra 00:00 til 23:59 for en given dato.
            //Hvordan:
            //Enumerable.Range(0, 24): Skaber en sekvens af tal fra 0 til 23.
            //.Select(hour => date.Date.AddHours(hour).ToString("HH:mm")): For hver time(hour), tilføj det til datoen(date.Date)
            //og konverter det til en streng i formatet "HH:mm".
            //.ToList(): Konverterer den genererede sekvens til en liste.
            return Enumerable.Range(0, 24).Select(hour => date.Date.AddHours(hour).ToString("HH:mm")).ToList();
        }

        // Block specific hours during a single day.
        private List<string> BlockSingleDay(BookedDays bookedDays)
        {
        //Genererer en liste over alle timer fra starttidspunktet til sluttidspunktet på en specifik dag.
        //Hvordan:  
        
            List<string> times = new List<string>();

            //new DateTime(bookedDays.StartDate.Year, bookedDays.StartDate.Month, bookedDays.StartDate.Day, bookedDays.StartDate.Hour, 0, 0): Initialiserer time til starttidspunktet på startdatoen.
            DateTime time = new DateTime(bookedDays.StartDate.Year, bookedDays.StartDate.Month, bookedDays.StartDate.Day,
                                         bookedDays.StartDate.Hour, 0, 0); 
            //while (time <= bookedDays.EndDate): Itererer fra starttidspunktet til sluttidspunktet.
            while (time <= bookedDays.EndDate)
            {   //times.Add(time.ToString("HH:mm")): Tilføjer hver time til listen i formatet "HH:mm".
                times.Add(time.ToString("HH:mm"));    
                //time = time.AddHours(1): Inkrementerer time med én time.
                time = time.AddHours(1);
            }
           
            return times;
        }






        // Block from the start of a booking to the end of the day.
        private List<string> BlockStartingDay(BookedDays bookedDays, DateTime date)
        {
            //Genererer en liste over alle timer fra starttidspunktet til slutningen af den pågældende dag.
            List<string> times = new List<string>();
            //Hvordan:
            //new DateTime(bookedDays.StartDate.Year, bookedDays.StartDate.Month, bookedDays.StartDate.Day, bookedDays.StartDate.Hour, 0, 0) : Initialiserer time til starttidspunktet på startdatoen.
            DateTime time = new DateTime(bookedDays.StartDate.Year, bookedDays.StartDate.Month, bookedDays.StartDate.Day,
                                         bookedDays.StartDate.Hour, 0, 0);
            //while (time<date.Date.AddDays(1)): Itererer fra starttidspunktet til slutningen af dagen.
            while (time < date.Date.AddDays(1))
            {
                //times.Add(time.ToString("HH:mm")): Tilføjer hver time til listen i formatet "HH:mm".
                times.Add(time.ToString("HH:mm"));
                //time = time.AddHours(1): Inkrementerer time med én time.
                time = time.AddHours(1);
            }
            return times;
        }

        // Block from the start of the day to the end time of a booking.
        private List<string> BlockEndingDay(BookedDays bookedDays, DateTime date)
        {
            //Genererer en liste over alle timer fra starten af dagen til sluttidspunktet på den pågældende dag.
            List<string> times = new List<string>();
            //Hvordan:
            //date.Date: Initialiserer time til starten af dagen.
            DateTime time = date.Date;
            //while (time <= bookedDays.EndDate): Itererer fra starten af dagen til sluttidspunktet.
            while (time <= bookedDays.EndDate)
            {
                //times.Add(time.ToString("HH:mm")): Tilføjer hver time til listen i formatet "HH:mm".
                times.Add(time.ToString("HH:mm"));
                //time = time.AddHours(1): Inkrementerer time med én time.
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
