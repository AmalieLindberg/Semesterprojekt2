using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Semesterprojekt2.Models;
using Semesterprojekt2.Models.BookATime;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using System.Runtime.Intrinsics.X86;

namespace Semesterprojekt2.Service.BookATimeService
{
    public class BookATimeService : IBookATimeService
    {
        private BookATimeDbService _bookATimeDbService {  get; set; }
        
        private BookedDaysService _bookedDaysService { get; set; }
        public BookATimeService(BookATimeDbService bookATimeDbService, BookedDaysService bookedDaysService)
        {
            _bookATimeDbService = bookATimeDbService;
            BookATimeList = _bookATimeDbService.GetObjectsAsync().Result.ToList();
            _bookedDaysService = bookedDaysService;
        }
        public List<BookATime> BookATimeList { get; set; }

        public async Task addTidsbestilling(BookATime bookATime)
        {
            BookATimeList.Add(bookATime);
            await _bookATimeDbService.AddObjectAsync(bookATime);

        }



        //public (int Year, int Month) AdjustYearAndMonth(int year, int month)
        //{ //Hvis enten year eller month er nul (0),
        //  //sættes year til det nuværende år og month til den nuværende måned ved hjælp af DateTime.Now.Year og DateTime.Now.Month
        //    if (year == 0 || month == 0)
        //    {
        //        year = DateTime.Now.Year;
        //        month = DateTime.Now.Month;
        //    }
        //    else
        //    { //Hvis month er mindre end 1
        //      //(altså en ugyldig måned som f.eks. 0 eller -1),
        //      //sættes month til 12 (den sidste måned i året),
        //      //og year reduceres med 1 for at afspejle, at vi er gået et år tilbage.
        //        if (month < 1)
        //        {
        //            month = 12;
        //            year -= 1;
        //        }
        //        //Hvis month er større end 12 (altså en ugyldig måned som f.eks. 13 eller 14), sættes month til 1
        //        //(den første måned i året), og year øges med 1 for at afspejle, at vi er gået et år frem.
        //        else if (month > 12)
        //        {
        //            month = 1;
        //            year += 1;
        //        }
        //    }
        //    //Metoden returnerer et tuppel (year, month), som repræsenterer det justerede år og måned.
        //    //tuppel betyder at man returner flere værdier
        //    return (year, month);
        //}
        public (int Year, int Month) AdjustYearAndMonth(int year, int month)
        {

            // Beregn den maksimale tilladte måned og år, som er tre måneder frem fra nu
            int currentMonth = DateTime.Now.Month;
            int currentYear = DateTime.Now.Year;
            // Hvis enten year eller month er nul (0), sættes year til det nuværende år
            // og month til den nuværende måned ved hjælp af DateTime.Now.Year og DateTime.Now.Month
            if (year == 0 || month == 0)
            {
                year = currentYear;
                month = currentMonth;
            }
            else
            {
                // Hvis month er mindre end 1 (altså en ugyldig måned som f.eks. 0 eller -1),
                // sættes month til 12 (den sidste måned i året), og year reduceres med 1 for at afspejle,
                // at vi er gået et år tilbage.
                if (month < 1)
                {
                    month = 12;
                    year -= 1;
                }
                // Hvis month er større end 12 (altså en ugyldig måned som f.eks. 13 eller 14),
                // sættes month til 1 (den første måned i året), og year øges med 1 for at afspejle,
                // at vi er gået et år frem.
                else if (month > 12)
                {
                    month = 1;
                    year += 1;
                }
            }
            //Denne linje lægger tre måneder til den aktuelle måned (currentMonth).
            //For eksempel, hvis den aktuelle måned er maj (måned 5), vil maxMonth blive 8 (august).
            int maxMonth = currentMonth + 3;
            //Denne linje sætter maxYear til det aktuelle år (currentYear).
            //Dette betyder, at hvis den aktuelle dato er i 2024, vil maxYear initialt blive sat til 2024.
            int maxYear = currentYear;

            // Hvis maxMonth overstiger 12 (december), juster maxMonth og maxYear tilsvarende
            if (maxMonth > 12)
            {
                maxMonth -= 12;
                maxYear += 1;
            }

            // Tjek, om den justerede måned og år overstiger den maksimale grænse
            // Hvis ja, sæt year og month til den maksimale måned og år
            if (year > maxYear || (year == maxYear && month > maxMonth))
            {
                year = maxYear;
                month = maxMonth;
            }

            // Metoden returnerer et tuppel (year, month), som repræsenterer det justerede år og måned
            return (year, month);
        }


        //Metoder 

        public async Task<List<string>> GetUpdatedAvailableTimes(DateTime date, List<string> additionalBookedTimes)
        {

            //List<string> er en generisk liste, som bruges til at holde en samling af string. Her bruges denne liste til at gemme bookede tider, som hentes fra databasen.
            List<string> bookedTimesFromDb = await _bookATimeDbService.GetBookedTimesForDate(date);
            //additionalBookedTimes er tom list men kan senere hen blive brugt til at holde en samling.
            //andre tider.
            //hvor vi kombiner det vi har hente fra database med det fra kalenderne.
            //.Distinct(): Fjerner dubletter, så hver tid kun optræder én gang.
            //.ToList(): Konverterer resultatet tilbage til en liste.
            //Variablen allBookedTimes indeholder en unik liste af alle bookede tider fra både databasen og den ekstra liste (som er tom).
            //så indeholder kun fra databasen.
            List<string> allBookedTimes = bookedTimesFromDb.Concat(additionalBookedTimes).Distinct().ToList();

            // Få den opdaterede liste over tilgængelige tider ved at passere alle bookede tider
            List<string> availableTimes = GetAvailableTimes(date, allBookedTimes);
            //Returner hele listen alle tidspunkter som er ledig.
            return availableTimes;
        }
     




        public List<string> GetServiceTimes(DayOfWeek day)
        {
            //Switch for hvilket dag skal havde hvilket tider
            switch (day)
            { //Hvis dagene er lør-tirsdag har man en list<string> med 3 tider
                case DayOfWeek.Saturday:
                case DayOfWeek.Sunday:
                case DayOfWeek.Monday:
                case DayOfWeek.Tuesday:
                    return new List<string> { "10:00", "11:00", "12:00" }; // Mobile service times
                    //ons-fre har man en list<string> med to tider
                case DayOfWeek.Wednesday:
                case DayOfWeek.Thursday:
                case DayOfWeek.Friday:
                    return new List<string> { "09:00", "13:00" }; // Salon service times
                    //Hvis noget går galt fx man har glemt en dag vil man havde en list uden noget.
                default:
                    return new List<string>(); // No services available
            }
        }

        public List<string> GetAvailableTimes(DateTime date, List<string> bookedTimes)
        {

            // Bestem, om den givne dato falder på en dag, hvor mobil service tilbydes.
            //isMobileServiceDay sættes til sandt (true),
            //hvis datoen er en lørdag, søndag, mandag eller tirsdag.
            bool isMobileServiceDay = date.DayOfWeek == DayOfWeek.Saturday ||
                                      date.DayOfWeek == DayOfWeek.Sunday ||
                                      date.DayOfWeek == DayOfWeek.Monday ||
                                      date.DayOfWeek == DayOfWeek.Tuesday;
            //Henter fra GetServiceTimes metode i denne klasse hvor den få en liste over standard tider for ugedagene.
            //Metoden GetServiceTimes kaldes med ugedagen (date.DayOfWeek) for at hente en liste over servicetider.
            //Resultatet gemmes i serviceTimes som er en list<string>.
            List<string> serviceTimes = GetServiceTimes(date.DayOfWeek);
            //Få en liste over tider, der er blokerede for den pågældende dato.
            //Metoden _bookedDaysService.GetBlockedTimesForDate(date) kaldes for at hente de blokerede tider.
            //Resultatet gemmes i blockedTimes som også er en list<string>
            List<string> blockedTimes = _bookedDaysService.GetBlockedTimesForDate(date);
            //Hvis det er en mobil servicedag og der er nogen bookede tider (hvilket vil sige den er true)
            //returneres en tom liste hvis noget er valgt)
            //isMobileServiceDay == true
            //bookedTimes.Any() noget i listen er valgt for os er det et tidspunkt.
            //Hvilket angiver, at der ikke er nogen tilgængelige tider.
            if (isMobileServiceDay && bookedTimes.Any())
                {
                    return new List<string>();  //sætter listen til en ny tom list.
            }
            //Returner en liste over tilgængelige tider, som ikke er blokerede eller bookede.
            //serviceTimes.Where(time => !blockedTimes.Contains(time) && !bookedTimes.Contains(time)):
            //Filtrerer servicetiderne, så kun tider, der ikke er i blockedTimes og bookedTimes, inkluderes.
            //.ToList(): Konverterer resultatet tilbage til en liste.
            return serviceTimes.Where(time => !blockedTimes.Contains(time) && !bookedTimes.Contains(time)).ToList();

            //Forklar serviceTimes.Where helt ud:

            //serviceTimes er en liste over alle mulige servicetider for den pågældende ugedag.
            //.Where(...) er en LINQ-metode, der bruges til at filtrere elementerne i en samling baseret på en betingelse.
            //time =>: Angiver, at vi arbejder med hvert element i serviceTimes, kaldet time.
            //!blockedTimes.Contains(time): Tjekker, om blockedTimes IKKE indeholder time.
            //Hvis blockedTimes indeholder time, vil !blockedTimes.Contains(time) være false.
            //&&: Logisk AND-operatør.Begge betingelser skal være sande for at inkludere time.
            //!bookedTimes.Contains(time): Tjekker, om bookedTimes IKKE indeholder time.
            //Hvis bookedTimes indeholder time, vil!bookedTimes.Contains(time) være false.
            //For hver time i serviceTimes, inkluderes time kun, hvis den ikke findes i blockedTimes og ikke findes i bookedTimes.
            //Resultatet er en liste over tider, som er tilgængelige for booking,
            //idet de ikke er blokerede eller allerede bookede.
        }




        //GetDayClass matcher sammen med GetUpdatedAvailableTimes så hvis listen er =0 så røde ellers dagene oragne og gul

        public async Task<string> GetColurDay(DateTime date, List<string> additionalBookedTimes)
        {
            //sætter dato for denne dag vi er i.
            DateTime today = DateTime.Today;
            // Få opdateret liste af tilgængelige tider for den givne dato
            //Kalder den asynkrone metode GetUpdatedAvailableTimes for at få en liste over tilgængelige tider
            //for den specifikke dato (date) med en ekstra liste over bookede tider (additionalBookedTimes).
            var availableTimes = await GetUpdatedAvailableTimes(date, additionalBookedTimes);
            //Ser om date som er angivet i parameter er efter i dag hvor den så sættes som "past-day" hvis sandt
            if (date < today)
            {
                return "past-day";
            }

            // Tjek om der ikke er nogen tilgængelige tider, altså om listen er tom
            if (availableTimes.Count == 0)
            {
                return "red-day";
            }

            // switch (date.DayOfWeek): Tjekker hvilken dag i ugen date er.
            switch (date.DayOfWeek)
            {
                //Hvis date.DayOfWeek er onsdag, torsdag eller fredag, returneres "orange-day".
                case DayOfWeek.Wednesday:
                case DayOfWeek.Thursday:
                case DayOfWeek.Friday:
                    return "orange-day";

                //Hvis date.DayOfWeek er lørdag, søndag, mandag eller tirsdag, returneres "yellow-day".

                case DayOfWeek.Saturday:
                case DayOfWeek.Sunday:
                case DayOfWeek.Monday:
                case DayOfWeek.Tuesday:
                    return "yellow-day";
                default:
                    //Hvis date.DayOfWeek af en eller anden grund ikke matcher nogen af de specificerede dage,
                    //kastes en InvalidOperationException med beskeden "Ugyldig dag i ugen".
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
                        { 
                            book.BathRoomImage = bookATime.BathRoomImage;
                        }
                       
                        await _bookATimeDbService.UpdateObjectAsync(book);
                        return book;
                    }
                }


            }
            return null;
        }
        public async Task<List<BookATime>> DeleteDogInBookATime(int dogId)
        {
            //Opret en liste til at samle elementer, der skal slettes
            List<BookATime> itemsToBeDeleted = new List<BookATime>();

            //Saml alle elementer, der matcher dogId
            foreach (BookATime bookATime in BookATimeList)
            {
                if (bookATime.DogId == dogId)
                {
                    itemsToBeDeleted.Add(bookATime);
                }
            }

            //Slet alle indsamlede elementer
            foreach (BookATime itemToBeDeleted in itemsToBeDeleted)
            {
                BookATimeList.Remove(itemToBeDeleted);
                await _bookATimeDbService.DeleteObjectAsync(itemToBeDeleted);
            }
            //Returner listen over slettede elementer
            return itemsToBeDeleted;
        }


        public async Task<List<BookATime>> DeleteUserInBookATime(int userid)
        {
            //Opret en liste til at samle elementer, der skal slettes
            List<BookATime> itemsToBeDeleted = new List<BookATime>();

            //Saml alle elementer, der matcher userid
            foreach (BookATime bookATime in BookATimeList)
            {
                if (bookATime.UserId == userid)
                {
                    itemsToBeDeleted.Add(bookATime);
                }
            }

            // Slet alle indsamlede elementer
            foreach (BookATime itemToBeDeleted in itemsToBeDeleted)
            {
                BookATimeList.Remove(itemToBeDeleted);
                await _bookATimeDbService.DeleteObjectAsync(itemToBeDeleted);
            }
            //Returner listen over slettede elementer
            return itemsToBeDeleted;
        }
    }
}
