namespace Semesterprojekt2.DAO
{
    public class ProductOrderDAO
    {
            public int OrderId { get; set; }
            public int ProductId { get; set; }
            public string ProductName { get; set; }
            public decimal Price { get; set; }
            public int UserId { get; set; }
            public string UserName { get; set; }
            public int Count { get; set; }
    }
}
