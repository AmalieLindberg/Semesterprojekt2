using Semesterprojekt2.MockData.Profil;
using Semesterprojekt2.Models;

namespace Semesterprojekt2.Service.UserService.UserService
{
    public class UserService : IUserService
    {
        public List<User> _users { get; set; }

		private JsonFileUserService JsonFileUserService { get; set; }

		public UserService(JsonFileUserService jsonFileUserService)
		{
			_users = MockUsers.GetMockUser();
			JsonFileUserService = jsonFileUserService;
			//_users = JsonFileUserService.GetJsonUsers().ToList();
			JsonFileUserService.SaveJsonUsers(_users);
		}

		public UserService()
		{
			_users = MockUsers.GetMockUser().ToList();
		}

		public void AddUser(User user)
        {
            _users.Add(user);
            JsonFileUserService.SaveJsonUsers(_users);
		}

        public User GetUser(int id)
        {
            foreach (User user in _users)
            {
                if (user.UserId == id)
                    return user;
            }

            return null;
        }

        public void UpdateUser(User user)
        {
            if (user != null)
            {
                foreach (User u in _users)
                {
                    if (u.UserId == user.UserId)
                    {
                        u.Name = user.Name;
                        u.Email = user.Email;
                        u.Password = user.Password;
                        u.Telefonnummer = user.Telefonnummer;
                    }
                }
                JsonFileUserService.SaveJsonUsers(_users);
			}
        }

		public User DeleteUser(int? userId)
		{
			User userToBeDeleted = null;
			foreach (User user in _users)
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
                JsonFileUserService.SaveJsonUsers(_users);
			}

			return userToBeDeleted;
		}

		public List<User> GetUsers() { return _users; }
    }
}
