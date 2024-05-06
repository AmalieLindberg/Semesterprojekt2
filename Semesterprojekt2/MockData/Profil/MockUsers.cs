using Microsoft.AspNetCore.Identity;
using Semesterprojekt2.Models;

namespace Semesterprojekt2.MockData.Profil
{
    public class MockUsers
    {
        private static PasswordHasher<string> passwordHasher = new PasswordHasher<string>();

        public static List<Users> users = new List<Users>
        {
            new Users(passwordHasher.HashPassword(null, "123"), "Admin", "12345678", "Email@email.dk", "Admin"),

        };


        public static List<Users> GetMockUser()
        {
            return users;
        }
    }
}
