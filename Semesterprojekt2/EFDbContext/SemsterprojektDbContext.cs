using Microsoft.EntityFrameworkCore;
using Semesterprojekt2.Models.BookATime;
using Semesterprojekt2.Models;
using Semesterprojekt2.Models.Shop;

namespace Semesterprojekt2.EFDbContext
{
    public class SemsterprojektDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(@"Data Source=mssql1.unoeuro.com;Initial Catalog=beckyblau_dk_db_shampoodle; User ID=beckyblau_dk; Password=BtahFDkEnwd5ey3RbH29; TrustServerCertificate=true");

        }
    
        public DbSet<BookATime> BookATime { get; set; }
        public DbSet<Users> Users { get; set; }
       
        public DbSet<Ydelse> Ydelse { get; set; }
        public DbSet<Dog> Dog { get; set; }
        public DbSet<Product> Product { get; set; }
        //public DbSet<CartItem> CartItems { get; set; }
        public DbSet<BookedDays> BookedDays { get; set; }
        public DbSet<ProductOrder> ProductOrder { get; set; }

    }
}
