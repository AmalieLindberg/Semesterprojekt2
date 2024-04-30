using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Semesterprojekt2.Models.BookATime
{
    public class BookATime
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? Kommentar { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}")]

        public DateTime DateForBooking { get; set; }

        public string? DogImage { get; set; }

        public string? BathRoomImage { get; set; }

        public bool? Elevator { get; set; }
        public string? Floor { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]

        public DateTime DateForOrder { get; set; }
        [Required]
        public int YdelseId { get; set; }
        public Ydelse Ydelse { get; set; }

        [Required]
        public int UserId { get; set; }
        public User User { get; set; }
        public string Status { get; set; }
        public BookATime(string kommentar, DateTime Date, Ydelse ydelse, User user)
        {

            Kommentar = kommentar;
            this.DateForBooking = Date;
            this.User = user;
            this.Ydelse = ydelse;


        }

        public BookATime()
        {
        }
    }
}
