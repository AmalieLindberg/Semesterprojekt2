﻿using Semesterprojekt2.Models.Shop;
using Semesterprojekt2.Models;

namespace Semesterprojekt2.Service.UserService.UserService
{
    public interface IUserService
    {
        List<Users> GetUsers();
        Task<Users> AddUser(Users user);
        Task<Users> UpdateUser(Users user);
        Users GetUser(int id);
        Task<Users> DeleteUser(int? userId);
      
        Users GetUserTidsbestillingOrders(Users currentUser);
        //Users GetUserByUserName(string name);

    }
}
