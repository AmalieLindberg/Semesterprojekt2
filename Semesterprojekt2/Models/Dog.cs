
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

        public string Name { get; set; }
        public DateTime Age { get; set; }
        public string Race { get; set; }
        public bool FirstTime { get; set; } // Fortsætter som en ikke-nullable bool

        public string FirstTimeDisplay => FirstTime ? "Yes" : "No";


        public Dog(string name, DateTime age, string race, bool firstTime)
        {
            Name = name;
            Age = age;
            Race = race;
            FirstTime = firstTime;
        }

        public Dog()
        {
        }
    }
}
