using Semesterprojekt2.MockData.Profil;
using Semesterprojekt2.Models;

namespace Semesterprojekt2.Service.UserService.UserService
{
    public class UserService : IUserService
    {
        public List<Users> _users { get; set; }

        private JsonFileUserService _jsonFileUserService { get; set; }

        public UserService(JsonFileUserService jsonFileUserService)
        {
            //_users = MockUsers.GetMockUser(); //Used to populate User.json once to create Admin Login
            
            _jsonFileUserService = jsonFileUserService;
            _users = jsonFileUserService.GetJsonUsers().ToList();
            _jsonFileUserService.SaveJsonUsers(_users);

        }

        public async Task AddUser(Users user)
        {
            if (!_users.Any())
                user.UserId = 1;
            else
                user.UserId = _users.Max(u => u.UserId) + 1;

            _users.Add(user);
            _jsonFileUserService.SaveJsonUsers(_users);
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

        public void UpdateUser(Users user)
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
                        u.Password = user.Password;
                        u.Telefonnummer = user.Telefonnummer;
                        u.Role = user.Role;
                    }
                }
                _jsonFileUserService.SaveJsonUsers(_users);
            }
        }

        public Users DeleteUser(int? userId)
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
                _jsonFileUserService.SaveJsonUsers(_users);
            }

            return userToBeDeleted;
        }

        public List<Users> GetUsers() 
        {
            _users = _jsonFileUserService.GetJsonUsers().ToList(); //reload data from JSON file when method is called.
            return _users; 
        }

		//public Users GetUserByUserName(string name)
		//{
		//	//return DbService.GetObjectByIdAsync(name).Result;
		//	return Users.Find(user => users.Name == name);
		//}
	}
}
