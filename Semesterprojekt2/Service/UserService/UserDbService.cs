using Microsoft.EntityFrameworkCore;
using Semesterprojekt2.EFDbContext;
using Semesterprojekt2.Models;
using Semesterprojekt2.DAO;

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

        public async Task<List<ProductOrderDAO>> GetOrdersByUserIdAsync(int id)
        {
            List<ProductOrderDAO> productOrderList = new List<ProductOrderDAO>();

            using (var context = new SemsterprojektDbContext())
            {

                var orders = from productOrder in context.ProductOrder
                             join product in context.Product on productOrder.ProductId equals product.Id
                             join users in context.Users on productOrder.UserId equals users.UserId
                             where users.UserId == id
                             select new ProductOrderDAO()
                             {
                                 OrderId = productOrder.OrderId,
                                 UserId = users.UserId,
                                 UserName = users.Name,
                                 ProductId = product.Id,
                                 ProductName = product.Name,
                                 Price = (decimal)product.Price,
                                 Count = productOrder.Count
                             };

                foreach (var order in orders)
                {
                    productOrderList.Add(order);
                }
            }

            return productOrderList;
        }
    }
}
