using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Semesterprojekt2.Models
{
    public class Tidsbestilling
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? Kommentar { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime Dato { get; set; }
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:HH:mm}")]
        public DateTime Tidspunkt { get; set; }

        public Tidsbestilling(string kommentar, DateTime Dato, DateTime Tidspunkt)
        {

            Kommentar = kommentar;
            this.Dato = Dato;
            this.Tidspunkt = Tidspunkt;


        }

        public Tidsbestilling()
        {
        }
    }
}
