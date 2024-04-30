using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Semesterprojekt2.Models
{
    public class User
    {
        //Vi skal ikke brug dem, da vi oprette database manuelt hvis vi har oprette den via Entity Framework.
        //havde vi brugt denne til at fortælle database hvad.
        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public int Telefonnummer { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        
        public string Role { get; set; }

        public User(string password, string name, int telefonnummer, string email)
        {
            Password = password;
            Name = name;
            Telefonnummer = telefonnummer;
            Email = email;
        }

        public User()
        {
         
            Password = "";
            Name = "";
            Telefonnummer = 0;
            Email = "";
        }


    }
}
