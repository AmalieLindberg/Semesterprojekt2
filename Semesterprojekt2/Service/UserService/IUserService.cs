using Semesterprojekt2.Models.Shop;
using Semesterprojekt2.Models;
using Semesterprojekt2.DAO;

namespace Semesterprojekt2.Service.UserService.UserService
{
    public interface IUserService
    {
        List<Users> GetUsers();
        Task<Users> AddUser(Users user);
        Task<Users> UpdateUser(Users user);
        Users GetUser(int id);
        Task<Users> DeleteUser(int? userId);
		Task<Users> ResetPassword(Users user);

		Users GetUserTidsbestillingOrders(Users currentUser);
        Users GetUserDogs(Users id);
        //IEnumerable<ProductOrderDAO> GetUserProductOrders(Users users);
        //Users GetUserByUserName(string name);

    }
}
