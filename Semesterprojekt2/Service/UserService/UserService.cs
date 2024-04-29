using Semesterprojekt2.MockData.Profil;
using Semesterprojekt2.Models;

namespace Semesterprojekt2.Service.UserService.UserService
{
    public class UserService : IUserService
    {
        public List<User> _users { get; set; }

        public UserService()
        {
            _users = MockUsers.GetMockUser();
        }

        public void AddUser(User user)
        {
            _users.Add(user);
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
            }
        }

        public User DeleteUser(int? userId)
        {
            foreach (User user in _users)
            {
                if (user.UserId == userId)
                {
                    _users.Remove(user);
                    return user;
                }
            }
            return null;
        }

        public List<User> GetUsers() { return _users; }
    }
}
