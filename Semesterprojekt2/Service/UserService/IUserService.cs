using Semesterprojekt2.Models.Shop;
using Semesterprojekt2.Models;

namespace Semesterprojekt2.Service.UserService.UserService
{
    public interface IUserService
    {
        List<User> GetUsers();
        void AddUser(User user);
        public void UpdateUser(User user);
        public User GetUser(int id);
        User DeleteUser(int? userId);
    }
}
