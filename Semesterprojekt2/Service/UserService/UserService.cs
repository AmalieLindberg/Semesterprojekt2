﻿using Microsoft.AspNetCore.Identity;
using Semesterprojekt2.MockData.Profil;
using Semesterprojekt2.Models;
using Semesterprojekt2.DAO;
using Semesterprojekt2.MockData.Profil;

namespace Semesterprojekt2.Service.UserService.UserService
{
    public class UserService : IUserService
    {
        private UserDbService DbService { get; set; }
        public List<Users> _users { get; set; }
        private JsonFileUserService jsonFileUserService { get; set; }

		private string genericPW = "ResetPW1234";

		private PasswordHasher<string> passwordHasher;


		public UserService(UserDbService userDbService, JsonFileUserService userService)
        {
            DbService = userDbService;
            jsonFileUserService = userService;
            //_users = MockUsers.GetMockUser(); //Used to populate User.json once to create Admin Login
            
            //_users = jsonFileUserService.GetJsonUsers().ToList();
            //DbService.SaveObjects(_users);


            _users = DbService.GetObjectsAsync().Result.ToList();

			//_jsonFileUserService.SaveJsonUsers(_users);

			passwordHasher = new PasswordHasher<string>();

		}

        public async Task<Users> AddUser(Users user)
        {
            /*
            if (!_users.Any())
                user.UserId = 1;
            else
                user.UserId = _users.Max(u => u.UserId) + 1;
            */

            _users.Add(user);
            await DbService.AddObjectAsync(user);
            return user;
        }



        public Users GetUser(int id)
        {
			
			foreach (Users user in _users)
            {
                if (user.UserId == id)
                    return user;
            }

            return null;
        }

        public async Task<Users> UpdateUser(Users user)
        {
            if (user != null)
            {
                foreach (Users u in _users)
                {
                    if (u.UserId == user.UserId)
                    {
                        u.UserId = user.UserId;
                        u.Name = user.Name;
                        u.Email = user.Email;
   
                        u.Telefonnummer = user.Telefonnummer;
						// Hash the password only if it's changed, and ensure it's being hashed
						if (!string.IsNullOrEmpty(user.Password))
						{
							u.Password = passwordHasher.HashPassword(null, user.Password);
							// Debug: Log or check the hash
							// Console.WriteLine($"Hashed Password: {u.Password}");
						}

						u.Role = user.Role;
                        if (user.ProfileImages== null)
                        {
                            user.ProfileImages = u.ProfileImages;
                        }
                        u.ProfileImages = user.ProfileImages;
                        u.Bio = user.Bio;

						await DbService.UpdateObjectAsync(u);
						return u;
					}
                }

				

			}

            return null;
        }

        public async Task<Users> DeleteUser(int? userId)
        {
            Users userToBeDeleted = null;
            foreach (Users user in _users)
            {
                if (user.UserId == userId)
                {
                    userToBeDeleted = user;
                    break;
                }
            }

            if (userToBeDeleted != null)
            {
                _users.Remove(userToBeDeleted);
                await DbService.DeleteObjectAsync(userToBeDeleted);
            }

            return userToBeDeleted;
        }

        public List<Users> GetUsers() 
        {
			_users = DbService.GetObjectsAsync().Result.ToList();
			return _users; 
        }
       
        public Users GetUserTidsbestillingOrders(Users user)
        {
            return DbService.GetTidsbestillingOrdersByUserIdAsync(user.UserId).Result;
        }

        public Users GetUserById(int id)
        {
            return DbService.GetObjectByIdAsync(id).Result;
        }
       public Users GetUserDogs(Users user)
        {
            return DbService.GetDogByUserIdAsync(user.UserId).Result;
        }
        public Users GetUserProductOrders(Users user)
        {
            return DbService.GetOrdersByUserIdAsync(user.UserId).Result;
        }

        //Reset Password Method

        public async Task<Users> ResetPassword(Users user)
		{
			if (user != null)
			{
				foreach (Users u in _users)
				{
					if (u.UserId == user.UserId)
					{
						u.Password = passwordHasher.HashPassword(null, genericPW);
						Console.WriteLine($"Hashed Password: {user.Password}");

						await DbService.UpdateObjectAsync(u);
						return u;

					}
				}

			}

			return null;

		}
        //public IEnumerable<ProductOrderDAO> GetUserProductOrders(Users users)
        //{
        //    return DbService.GetOrdersByUserIdAsync(users.UserId).Result;
        //}


    }
}
