using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Semesterprojekt2.Models.Shop;

namespace Semesterprojekt2.Models
{
    public class Orders
    {
		public int OrderId { get; set; }

		[Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
		public int Count { get; set; }

		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
		public DateTime Date { get; set; }

		[Required]
		public int UserId { get; set; }

		public Users Users { get; set; }

		[Required]
		public int ItemId { get; set; }

		public Product Product { get; set; }

		public Orders()
		{

		}

		public Orders(Users users, Product product)
		{
			Date = DateTime.Now;
			Users = users;
			Product = product;
		}
	}
}
