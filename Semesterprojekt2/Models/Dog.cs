
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Semesterprojekt2.Models
{
    public class Dog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }
        public DateTime Age { get; set; }
        public string Race { get; set; }
        public bool FirtsTime { get; set; } //First time with a dog grommer



    }
}
