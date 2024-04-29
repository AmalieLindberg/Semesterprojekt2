using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Semesterprojekt2.Models.BookATime;

namespace Semesterprojekt2.Service
{

    public class YdelseService
    {

        public List<Ydelse> Ydelses { get; set; }
        public YdelseService()
        {


        }

        public void AddYdelseAsync(Ydelse ydelse)
        {
            Ydelses.Add(ydelse);


        }

    }
}
