using Microsoft.EntityFrameworkCore;
using Semesterprojekt2.EFDbContext;
using Semesterprojekt2.Models;

namespace Semesterprojekt2.Service.UserService
{
    public class UserDbService : DbGenericService<Users>
    {
        public async Task<Users> GetTidsbestillingOrdersByUserIdAsync(int id)
        {
            Users user;

            using (var context = new SemsterprojektDbContext())
            {
                user = context.Users
                .Include(u => u.BookATime)
                .ThenInclude(i => i.Ydelse)
                 .Include(u => u.BookATime)
                 .ThenInclude(i => i.Dog)
                .AsNoTracking()
                .FirstOrDefault(u => u.UserId == id);
            }

            return user;
        }

        public async Task<Users> GetDogByUserIdAsync(int userId)
        {
            Users user;

            using (var context = new SemsterprojektDbContext())
            {
                user = context.Users
                .Include(u => u.Dog)
                .AsNoTracking()
                .FirstOrDefault(u => u.UserId == userId);
            }

            return user;
        }
    }
}
