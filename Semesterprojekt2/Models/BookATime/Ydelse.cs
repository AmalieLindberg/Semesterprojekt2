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

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public DogSize HundeStørrelse { get; set; }
        [Required]
        public ServiceType Type { get; set; }

        public static int id = 1;


        public Ydelse(DogSize HundeStørrelse, ServiceType Type)
        {

            this.HundeStørrelse = HundeStørrelse;
            this.Type = Type;
            Id = id++;

        }
        public Ydelse()
        {



        }
    }
}
