using Microsoft.EntityFrameworkCore;
using Semesterprojekt2.Models.BookATime;
using Semesterprojekt2.Models.Shop;
namespace Semesterprojekt2.Service
{
    public class ProductOrderService
    {
        public List<ProductOrder> OrderList { get; set; } = new List<ProductOrder>();

        public DbGenericService<ProductOrder> DbService { get; set; }

        public ProductOrderService(DbGenericService<ProductOrder> dbService)
        {
            DbService = dbService;
            OrderList = DbService.GetObjectsAsync().Result.ToList();
        }

        public async Task AddOrderAsync(ProductOrder productOrder)
        {
        
            OrderList.Add(productOrder);
            await DbService.AddObjectAsync(productOrder);
        }

        public IEnumerable<ProductOrder>? GetAllProductOrders()
        { 
            return OrderList; 
        }

        public ProductOrder GetProductOrderById(int id)
        {
            foreach (var order in OrderList)
            {
                if (id == order.OrderId)
                    return order;
            }
            return null;
        }

    }
}
