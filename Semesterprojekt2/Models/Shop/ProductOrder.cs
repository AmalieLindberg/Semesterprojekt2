using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Semesterprojekt2.Models.Shop
{
    public class ProductOrder
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
        public int ProductId { get; set; }

        public Product Product { get; set; }

        public ProductOrder()
        {

        }

        public ProductOrder(Users users, Product product)
        {
            Date = DateTime.Now;
            Users = users;
            Product = product;
        }
    }
}
