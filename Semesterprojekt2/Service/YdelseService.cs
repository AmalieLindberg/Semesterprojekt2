using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Semesterprojekt2.Models.BookATime;

namespace Semesterprojekt2.Service
{

    public class YdelseService
    {
        private DbGenericService<Ydelse> DbGenericService { get; set; }
        public List<Ydelse> Ydelses { get; set; }
        public YdelseService(DbGenericService<Ydelse> dbGenericService)
        {
            DbGenericService = dbGenericService;
            Ydelses = DbGenericService.GetObjectsAsync().Result.ToList();
        }

        public void AddYdelseAsync(Ydelse ydelse)
        {
            Ydelses.Add(ydelse);


        }

    }
}
