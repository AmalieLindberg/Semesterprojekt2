using Semesterprojekt2.Models.BookATime;

namespace Semesterprojekt2.Service.BookATimeService
{
    public class BookedDaysService
    {
        //private DbGenericService<BookedDays> _BookedDaysDbService { get; set; }
        private List<BookedDays> blokeredeDage;

        public BookedDaysService(/*DbGenericService<BookedDays> bookedDaysDbService*/)
        {
            blokeredeDage = new List<BookedDays>();
            //_BookedDaysDbService = bookedDaysDbService;
            //blokeredeDage = _BookedDaysDbService.GetObjectsAsync().Result.ToList();
        }

        public async Task AddBookedDays(DateTime start, DateTime slut)
        {
            blokeredeDage.Add(new BookedDays { StartDate = start, EndDate = slut });
            //await _BookedDaysDbService.AddObjectAsync(new BookedDays { StartDate = start, EndDate = slut });
        }
        public async Task RemoveBookedDays(int id)
        {
            foreach (var bookedDays in blokeredeDage)
            {
                if (bookedDays.Id == id) { 
               blokeredeDage.Remove(bookedDays);
                //await _BookedDaysDbService.DeleteObjectAsync(ferieTilSletning);
                }
            }
            

        }

        public List<BookedDays> GetBookedDaysList()
        {
            return blokeredeDage;
        }
        public async Task RevomeBookedDays(BookedDays bookedDays)
        {

            blokeredeDage.Remove(bookedDays);
            //await _BookedDaysDbService.DeleteObjectAsync(bookedDays);

        }
        //    public async Task OpdaterFerie(DateTime originalStart, DateTime originalSlut, DateTime sletStart, DateTime sletSlut)
        //{
        //    var ferie = blokeredeDage.FirstOrDefault(ferie => ferie.StartDato == originalStart && ferie.EndDato == originalSlut);
        //    if (ferie != null)
        //    {
        //        // Fjern den originale ferieperiode
        //        blokeredeDage.Remove(ferie);
        //        await _FerieDbService.DeleteObjectAsync(ferie);

        //        // Tilføj ny(e) periode(r) hvis nødvendig
        //        if (ferie.StartDato < sletStart)
        //        {
        //            blokeredeDage.Add(new Ferie { StartDato = ferie.StartDato, EndDato = sletStart.AddDays(-1) });
        //            await _FerieDbService.AddObjectAsync(ferie);
        //        }
        //        if (ferie.EndDato > sletSlut)
        //        {
        //            blokeredeDage.Add(new Ferie { StartDato = sletSlut.AddDays(1), EndDato = ferie.EndDato });
        //            await _FerieDbService.AddObjectAsync(ferie);
        //        }
        //    }
        //}
        public bool ErDatoBlokeret(DateTime dato)
        {
            return blokeredeDage.Any(ferie => dato >= ferie.StartDate && dato <= ferie.EndDate);
        }
        //public bool ErDatoBlokeret(DateTime dato)
        //{
        //    foreach (var ferie in blokeredeDage)
        //    {
        //        if (dato >= ferie.StartDato && dato <= ferie.EndDato)
        //        {
        //            return true;
        //        }
        //    }
        //    return false;
        //}
    }
}
