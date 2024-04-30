using Semesterprojekt2.Models.Shop;
using Semesterprojekt2.Models;

namespace Semesterprojekt2.Service.UserService.UserService
{
    public interface IUserService
    {
        List<Users> GetUsers();
        void AddUser(Users user);
        public void UpdateUser(Users user);
        public Users GetUser(int id);
        Users DeleteUser(int? userId);
    }
}
