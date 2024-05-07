﻿using Microsoft.EntityFrameworkCore;
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
    
        public DbSet<BookATime> BookATimes { get; set; }
        public DbSet<Users> Users { get; set; }
       
        public DbSet<Ydelse> Ydelses { get; set; }
        public DbSet<Dog> Dogs { get; set; }
        public DbSet<Product> Product { get; set; }
        //public DbSet<CartItem> CartItems { get; set; }
        public DbSet<BookedDays> BookedDays { get; set; }
     
    }
}
