using Semesterprojekt2.MockData.OrderHis;
using Semesterprojekt2.Models;

namespace Semesterprojekt2.Service
{
    public class OrderService
    {
        public List<Orders> OrderList { get; set; }

        public OrderService()
        {
            OrderList = MockOrder.GetMockOrder();
        }

        public async void AddOrder(Orders order)
        {
            OrderList.Add(order);
        }

    }
}
