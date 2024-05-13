using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Semesterprojekt2.Models.BookATime;
using Semesterprojekt2.Models;
using System.Diagnostics;

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

        public async Task<Ydelse> AddYdelseAsync(Ydelse ydelse)
        {
            Ydelses.Add(ydelse);
            await DbGenericService.AddObjectAsync(ydelse);
            return ydelse;

        }

        public Ydelse? GetYdelseById(int id)
        {
            foreach (var ydelse in Ydelses)
            {
                if (id == ydelse.Id)
                    return ydelse;

            }
            return null;
        }
        public async Task<Ydelse> UpdateYdelse(Ydelse ydelse)
        {
            if (ydelse != null)
            {
                foreach (Ydelse y in Ydelses)
                {
                    if (y.Id == ydelse.Id)
                    {
                        y.DogSize = ydelse.DogSize;
                        y.ServiceType = ydelse.ServiceType;
                        await DbGenericService.UpdateObjectAsync(y);
                        return y;
                    }
                }


            }
            return null;
        }
    }
}
