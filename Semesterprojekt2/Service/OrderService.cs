using Semesterprojekt2.MockData.OrderHis;
using Semesterprojekt2.Models;

namespace Semesterprojekt2.Service
{
    public class OrderService
    {
        public List<Order> OrderList { get; set; }

        public OrderService()
        {
            OrderList = MockOrder.GetMockOrder();
        }

        public async void AddOrder(Order order)
        {
            OrderList.Add(order);
        }

    }
}
