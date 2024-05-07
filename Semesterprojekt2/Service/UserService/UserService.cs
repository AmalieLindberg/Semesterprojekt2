using Semesterprojekt2.MockData.Profil;
using Semesterprojekt2.Models;

namespace Semesterprojekt2.Service.UserService.UserService
{
    public class UserService : IUserService
    {
        private UserDbService DbService { get; set; }
        public List<Users> _users { get; set; }

 

        public UserService(UserDbService userDbService)
        {
            //_users = MockUsers.GetMockUser(); //Used to populate User.json once to create Admin Login
            DbService = userDbService;
           
            //_users = jsonFileUserService.GetJsonUsers().ToList();
            //DbService.SaveObjects(_users);

            _users = DbService.GetObjectsAsync().Result.ToList();

            //_jsonFileUserService.SaveJsonUsers(_users);

        }

        public async Task AddUser(Users user)
        {
            if (!_users.Any())
                user.UserId = 1;
            else
                user.UserId = _users.Max(u => u.UserId) + 1;

            _users.Add(user);
            DbService.AddObjectAsync(user);
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
                DbService.UpdateObjectAsync(user);
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
                DbService.DeleteObjectAsync(userToBeDeleted);
            }

            return userToBeDeleted;
        }

        public List<Users> GetUsers() 
        {
            return _users; 
        }

		//public Users GetUserByUserName(string name)
		//{
		//	//return DbService.GetObjectByIdAsync(name).Result;
		//	return Users.Find(user => users.Name == name);
		//}
	}
}
