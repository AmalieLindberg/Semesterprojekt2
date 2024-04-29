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
            options.UseSqlServer(@"Data Source=mssql1.unoeuro.com;Initial Catalog=beckyblau_dk_db_shampoodle; Integrated Security=True; Connect Timeout=30; Encrypt=False");

        }

        public DbSet<BookATime> BookATimes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Ydelse> Ydelses { get; set; }
        public DbSet<Dog> Dogs { get; set; }
        public DbSet<Product> Products { get; set; }
     
    }
}
