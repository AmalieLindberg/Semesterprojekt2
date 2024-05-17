using System.ComponentModel.DataAnnotations;

namespace Semesterprojekt2.Models.Shop
{
    public class ProductOrder
    {
        [Key]
        public int OrderId { get; set; }
        public int Counts { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime Date { get; set; }
        [Required]

        public int UserId { get; set; }
        public Users? User { get; set; }

        [Required]
        public int ProductId { get; set; }
       public Product? Product { get; set; }

        public ProductOrder()
        {
           
        }

        public ProductOrder(int Counts, Product product, Users user)
        {
            this.Counts = Counts;
            Date = DateTime.Now;
            User = user;
            Product = product;
         

        }

        public override string ToString()
        {
            return $"{{{nameof(OrderId)}={OrderId.ToString()}, {nameof(Counts)}={Counts.ToString()}, {nameof(Date)}={Date.ToString()}, {nameof(UserId)}={UserId.ToString()}, {nameof(ProductId)}={ProductId.ToString()}}}";
        }
    }
}
