using Semesterprojekt2.Models;

namespace Semesterprojekt2.MockData.OrderHis
{
    public class MockOrder
    {
        public static List<Orders> orders = new List<Orders>
        {
            new Orders("Hel")

        };
         
        public static List<Orders> GetMockOrder()
        {
            return orders;
        }
    }
}
