using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Semesterprojekt2.Models
{
    public class Users
    {
        //Vi skal ikke brug dem, da vi oprette database manuelt hvis vi har oprette den via Entity Framework.
        //havde vi brugt denne til at fortælle database hvad.
        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int UserId{ get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public string Telefonnummer { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        
        public string Role { get; set; }


        public Users(string password, string name, string telefonnummer, string email, string role)
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
            Telefonnummer = "";
            Email = "";
            Role = "";
        }


    }
}
