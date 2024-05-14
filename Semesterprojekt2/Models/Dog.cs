
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Semesterprojekt2.Models
{
    public class Dog
    {
        //Vi skal ikke brug dem, da vi oprette database manuelt hvis vi har oprette den via Entity Framework.
        //havde vi brugt denne til at fortælle database hvad.
        [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
		public string? DogImage { get; set; }
		public string Name { get; set; }
        public DateTime Age { get; set; }
        public string Race { get; set; }
        [Required]
        public int UserId { get; set; }
        public Users? User { get; set; }



        public Dog(string name, DateTime age, string race)
        {
            Name = name;
            Age = age;
            Race = race;
          
        }

        public Dog()
        {
        }
    }
}
