using Semesterprojekt2.Models.BookATime;

namespace Semesterprojekt2.Service.BookATimeService
{
    public class BookedDaysService
    {
        //private DbGenericService<BookedDays> _BookedDaysDbService { get; set; }
        private List<BookedDays> _bookedDays;

        public BookedDaysService(/*DbGenericService<BookedDays> bookedDaysDbService*/)
        {
            _bookedDays = new List<BookedDays>();
            //_BookedDaysDbService = bookedDaysDbService;
            //blokeredeDage = _BookedDaysDbService.GetObjectsAsync().Result.ToList();
        }

        public async Task AddBookedDays(DateTime start, DateTime slut)
        {
            _bookedDays.Add(new BookedDays { StartDate = start, EndDate = slut });
            //await _BookedDaysDbService.AddObjectAsync(new BookedDays { StartDate = start, EndDate = slut });
        }
        public async Task<BookedDays> RemoveBookedDaysById(int id)
        {
            foreach (var bookedDays in _bookedDays)
            {
                if (bookedDays.Id == id) {
                    _bookedDays.Remove(bookedDays);
                    //await _BookedDaysDbService.DeleteObjectAsync(bookedDays); 
                    return bookedDays;
                }
            }
            return null;

        }

        public List<BookedDays> GetBookedDaysList()
        {
            return _bookedDays;
        }
        public async Task RevomeBookedDays(BookedDays bookedDays)
        {

            _bookedDays.Remove(bookedDays);
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
            return _bookedDays.Any(ferie => dato >= ferie.StartDate && dato <= ferie.EndDate);
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
