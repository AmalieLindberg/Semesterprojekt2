using Semesterprojekt2.Models.Shop;
using Semesterprojekt2.Models;

namespace Semesterprojekt2.Service.UserService.UserService
{
    public interface IUserService
    {
        List<Users> GetUsers();
        Task AddUser(Users user);
        void UpdateUser(Users user);
        Users GetUser(int id);
        Users DeleteUser(int? userId);
        //Users GetUserByUserName(string name);

	}
}
