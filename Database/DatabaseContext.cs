using Cs_ProductApi.Models;
using Microsoft.EntityFrameworkCore;

namespace Cs_ProductApi.Database
{
    public class DatabaseContext: DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options): base(options){}

        public  DbSet<Product> Products { get; set; }
        public  DbSet<Category> Categories { get; set; }
    

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Primary Key
            //modelBuilder.Entity<Product>().HasKey(p => p.ProductID);
            modelBuilder.Entity<Category>().HasKey(p => p.CategoryID);
        }
    }
}
