using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Semesterprojekt2.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public int Telefonnummer { get; set; }
        [Required]
        public string Email { get; set; }
        
        public string Role { get; set; }

        public User(string userName, string password, string name, int telefonnummer, string email)
        {
            UserName = userName;
            Password = password;
            Name = name;
            Telefonnummer = telefonnummer;
            Email = email;
        }

        public User()
        {
            UserName = "";
            Password = "";
            Name = "";
            Telefonnummer = 0;
            Email = "";
        }


    }
}
