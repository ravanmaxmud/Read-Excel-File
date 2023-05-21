using Microsoft.EntityFrameworkCore;
using Read_Excel_File.Database.Models;
using Read_Excel_File.Extensions;

namespace Read_Excel_File.Database
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options)
       : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Model> Models { get; set; }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<BasketProduct> BasketProducts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly<Program>();
        }
    }
}
