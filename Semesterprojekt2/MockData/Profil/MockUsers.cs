using Semesterprojekt2.Models;

namespace Semesterprojekt2.MockData.Profil
{
    public class MockUsers
    {
        public static List<User> users = new List<User>
        {
            new User("Admin", "123", "Admin", 12345678, "Email@email.dk" )

        };

        public static List<User> GetMockUser()
        {
            return users;
        }
    }
}
