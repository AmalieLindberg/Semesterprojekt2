using Microsoft.EntityFrameworkCore;
using Semesterprojekt2.Models.BookATime;
using Semesterprojekt2.Models.Shop;
using Semesterprojekt2.Service.BookATimeService;
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
            // Tilføjer ordren til den lokale liste
            OrderList.Add(productOrder);
            // Tilføjer ordren til databasen
            await DbService.AddObjectAsync(productOrder);
        }

        public IEnumerable<ProductOrder>? GetAllProductOrders()
        { 
            return OrderList; 
        }

        public ProductOrder GetProductOrderById(int id)
        {
            // Gennemløber listen af ordrer og returnerer den, der matcher det givne ID
            foreach (var order in OrderList)
            {
                if (id == order.OrderId)
                    return order;
            }
            return null;
        }

        public async Task<List<ProductOrder>> DeleteUserInProductOrder(int userid)
        {
            // Liste til at holde de ordrer, der skal slettes
            List<ProductOrder> itemsToBeDeleted = new List<ProductOrder>();

            // Finder alle ordrer, der matcher brugerens ID
            foreach (ProductOrder productOrder in OrderList)
            {
                if (productOrder.UserId == userid)
                {
                    itemsToBeDeleted.Add(productOrder);
                }
            }

            // Sletter de fundne ordrer fra listen og databasen
            foreach (ProductOrder itemToBeDeleted in itemsToBeDeleted)
            {
                OrderList.Remove(itemToBeDeleted);
                await DbService.DeleteObjectAsync(itemToBeDeleted);
            }

            return itemsToBeDeleted;
        }

		public async Task<ProductOrder> DeleteProductOrderAsync(int? orderId)
		{
            // Variabel til at holde den ordre, der skal slettes
            ProductOrder productOrdersToBeDeleted = null;

            // Finder ordren, der matcher det givne ID
            foreach (ProductOrder productOrder in OrderList)
			{
				if (productOrder.OrderId == orderId)
				{
					productOrdersToBeDeleted = productOrder;
					break;
				}
			}

            // Sletter den fundne ordre fra listen og databasen
            if (productOrdersToBeDeleted != null)
			{
				OrderList.Remove(productOrdersToBeDeleted);
				await DbService.DeleteObjectAsync(productOrdersToBeDeleted);
			}

			return productOrdersToBeDeleted;
		}

	}
}
