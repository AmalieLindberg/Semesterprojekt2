using Semesterprojekt2.MockData.Profil;
using Semesterprojekt2.Models;

namespace Semesterprojekt2.Service
{
    public class UserService
    {
        public List<User> Users { get; set; }

        public UserService()
        {
            Users = MockUsers.GetMockUser();
        }
    }
}
