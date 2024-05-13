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

        public async void AddOrder(ProductOrder productOrder)
        {
            OrderList.Add(productOrder);
            await DbService.AddObjectAsync(productOrder);
        }
    }
}
