using System.ComponentModel.DataAnnotations;

namespace Semesterprojekt2.Models.Shop
{
    public class ProductOrder
    {
        // Primær nøgle for ProductOrder objektet
        [Key]
        public int OrderId { get; set; }

        // Antal af produkter i ordren
        public int Counts { get; set; }

        // Datoen for ordren, formateret som år-måned-dag
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime Date { get; set; }

        // Bruger ID for den bruger, der har lavet ordren (påkrævet)
        [Required]
        public int UserId { get; set; }

        public Users? User { get; set; }

        // Produkt ID for det bestilte produkt (påkrævet)
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
    }
}
