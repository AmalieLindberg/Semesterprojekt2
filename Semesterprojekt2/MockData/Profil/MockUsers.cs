using Semesterprojekt2.Models;

namespace Semesterprojekt2.MockData.Profil
{
    public class MockUsers
    {
        public static List<Users> users = new List<Users>
        {
            new Users("123", "Admin", "12345678", "Email@email.dk", "Admin"),
            new Users("123", "User", "12345678", "Email2@email.dk", "normaluser")

        };

        public static List<Users> GetMockUser()
        {
            return users;
        }
    }
}
