using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Semesterprojekt2.Models.BookATime
{
    public class BookATime
    {
        //Vi skal ikke brug dem, da vi oprette database manuelt hvis vi har oprette den via Entity Framework.
        //havde vi brugt denne til at fortælle database hvad.
        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? Kommentar { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}")]

        public DateTime DateForBooking { get; set; }

        public string? DogImage { get; set; }

        public string? BathRoomImage { get; set; }

        public bool? Elevator { get; set; }
        public string? Floors { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]

        public DateTime DateForOrder { get; set; }
        [Required]
        public int YdelseId { get; set; }
        public Ydelse Ydelse { get; set; }

        [Required]
        public int UserId { get; set; }
        public User User { get; set; }

        [Required]
        public int DogId { get; set; }
        public Dog Dog { get; set; }
        public string StatusForBooking { get; set; }
        public BookATime(string kommentar, DateTime Date, Ydelse ydelse, User user, Dog dog)
        {

            Kommentar = kommentar;
            this.DateForBooking = Date;
            this.User = user;
            this.Ydelse = ydelse;
            this.Dog = dog;

        }

        public BookATime()
        {
        }
    }
}
