using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Semesterprojekt2.Models.BookATime;

namespace Semesterprojekt2.Models
{
    public class Users
    {
        //Vi skal ikke brug dem, da vi oprette database manuelt hvis vi har oprette den via Entity Framework.
        //havde vi brugt denne til at fortælle database hvad.
        [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int UserId{ get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public int? Telefonnummer { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        
        public string Role { get; set; }

        public string? ProfileImages { get; set; }
        public string? Bio {  get; set; }
        public IEnumerable<Models.BookATime.BookATime>? BookATime { get; set; }

        public IEnumerable<Models.Dog>? Dog { get; set; }
        public Users(string password, string name, int telefonnummer, string email, string role)
        {
            Password = password;
            Name = name;
            Telefonnummer = telefonnummer;
            Email = email;
            Role = role;
        }

        public Users()
        {
         
            Password = "";
            Name = "";
            Telefonnummer=0;
            Email = "";
            Role = "";
        }


    }
}
