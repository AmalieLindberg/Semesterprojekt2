using Semesterprojekt2.Models;

namespace Semesterprojekt2.MockData.OrderHis
{
    public class MockOrder
    {
        public static List<Order> orders = new List<Order>
        {
            new Order("Hel")

        };
         
        public static List<Order> GetMockOrder()
        {
            return orders;
        }
    }
}
