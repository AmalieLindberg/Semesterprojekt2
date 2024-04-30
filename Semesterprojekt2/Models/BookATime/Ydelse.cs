using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Semesterprojekt2.Models.BookATime
{
    public enum DogSize
    {
        [Display(Name = "Small dog")]
        Small,
        [Display(Name = "Medium size dog")]
        Medium
    }

    public enum ServiceType
    {
        [Display(Name = "Short-haired")]
        ShortHaired,
        [Display(Name = "Short-haired cut to a style or breed standard style")]
        ShortHairedStyle,
        [Display(Name = "Long-haired cut to a style or breed standard style")]
        LongHairedStyle
    }

    public class Ydelse
    {

        //Vi skal ikke brug dem, da vi oprette database manuelt hvis vi har oprette den via Entity Framework.
        //havde vi brugt denne til at fortælle database hvad.
        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public DogSize DogSize { get; set; }
        [Required]
        public ServiceType ServiceType { get; set; }

        public static int id = 1;


        public Ydelse(DogSize Dogsize, ServiceType serviceType)
        {

            this.DogSize = Dogsize;
            this.ServiceType = serviceType;
            Id = id++;

        }
        public Ydelse()
        {



        }
    }
}
