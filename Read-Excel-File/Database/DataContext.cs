using Microsoft.EntityFrameworkCore;
using Read_Excel_File.Database.Models;
using ReadExcel.Extensions;

namespace ReadExcel.Database
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options)
       : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Model> Models { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly<Program>();
        }
    }
}
