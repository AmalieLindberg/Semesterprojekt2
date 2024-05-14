using Microsoft.EntityFrameworkCore;
using Semesterprojekt2.Models.Shop;
namespace Semesterprojekt2.Service
{
    public class ProductOrderService
    {
        public List<ProductOrder> OrderList { get; set; }

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

        public async Task SaveOrderAsync(ProductOrder productOrder)
        {
            // Add the order to the list and save it in the database
            await AddOrderAsync(productOrder);
        }

    }
}
